using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Enable.HealthCheck
{
    public class HealthCheckMiddleware
    {
        private readonly HealthCheckOptions _options;

        public HealthCheckMiddleware(
            RequestDelegate next,
            HealthCheckOptions options)
        {
            _options = options;
        }

        public async Task Invoke(
            HttpContext context,
            IHealthCheckService healthService,
            IHealthCheckErrorHandler errorHandler)
        {
            var requestPath = context.Request.Path.Value.TrimStart('/');

            if (_options.DisableEndpointPathCheck ||
                (_options.EndpointPathEnabled && string.Equals(requestPath, _options.EndpointPath)))
            {
                // App is running with health endpoint path check disabled or
                //   health check endpoint path is configured and
                //   the request URL path matches the configured health check endpoint path.
                try
                {
                    // Perform health checks against each required service.
                    await healthService.CheckAll();
                }
                catch (Exception ex)
                {
                    // Handle the error, so service issues can be diagnosed.
                    await errorHandler.HandleError(ex);

                    // Signal the error to the monitoring service.
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return;
                }

                // If we're here then all health checks above were successful.
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                return;
            }

            // If we're here then we're not in development mode and the health check endpoint is not configured
            // or the endpoint path specified in the request was incorrect.
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
