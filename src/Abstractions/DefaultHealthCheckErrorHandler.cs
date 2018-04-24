using System;
using System.Threading.Tasks;

namespace Enable.HealthCheck
{
    public class DefaultHealthCheckErrorHandler : IHealthCheckErrorHandler
    {
        public Task HandleError(Exception ex)
        {
            return Task.CompletedTask;
        }
    }
}
