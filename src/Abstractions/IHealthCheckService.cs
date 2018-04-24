using System.Threading.Tasks;

namespace Enable.HealthCheck
{
    public interface IHealthCheckService
    {
        Task CheckAll();
    }
}
