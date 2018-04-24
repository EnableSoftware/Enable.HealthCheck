using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Enable.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(
            this IServiceCollection services,
            HealthCheckOptions options = null)
        {
            return services.AddHealthCheck<DefaultHealthCheckService, DefaultHealthCheckErrorHandler>(options);
        }

        public static IServiceCollection AddHealthCheck<THealthService, TErrorHandler>(
            this IServiceCollection services,
            HealthCheckOptions options = null)
            where THealthService : class, IHealthCheckService
            where TErrorHandler : class, IHealthCheckErrorHandler
        {
            services.AddSingleton(options ?? new HealthCheckOptions());
            services.AddScoped<IHealthCheckService, THealthService>();
            services.AddScoped<IHealthCheckErrorHandler, TErrorHandler>();

            return services;
        }

        public static IApplicationBuilder UseHealthCheck(
            this IApplicationBuilder app,
            string path = "/health-check")
        {
            return app.Map(path, healthCheckApp =>
            {
                healthCheckApp.UseMiddleware<HealthCheckMiddleware>();
            });
        }
    }
}
