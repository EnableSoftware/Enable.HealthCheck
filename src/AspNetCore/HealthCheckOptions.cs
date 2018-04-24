namespace Enable.HealthCheck
{
    public class HealthCheckOptions
    {
        public bool DisableEndpointPathCheck { get; set; }
        public string EndpointPath { get; set; }
        public bool EndpointPathEnabled => !string.IsNullOrWhiteSpace(EndpointPath);
    }
}
