using System.Threading.Tasks;

namespace Enable.HealthCheck
{
    public class DefaultHealthCheckService : IHealthCheckService
    {
        public Task CheckAll()
        {
            return Task.CompletedTask;
        }
    }
}
