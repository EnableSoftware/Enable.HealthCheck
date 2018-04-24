using System;
using System.Threading.Tasks;
using Exceptionless;

namespace Enable.HealthCheck
{
    public class ExceptionlessErrorHandler : IHealthCheckErrorHandler
    {
        public Task HandleError(Exception ex)
        {
            ex.ToExceptionless()
                .MarkAsCritical()
                .AddTags("HealthCheck")
                .Submit();

            return Task.CompletedTask;
        }
    }
}
