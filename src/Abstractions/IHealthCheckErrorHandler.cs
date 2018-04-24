using System;
using System.Threading.Tasks;

namespace Enable.HealthCheck
{
    public interface IHealthCheckErrorHandler
    {
        Task HandleError(Exception ex);
    }
}
