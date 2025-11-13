using Microsoft.Extensions.Configuration;

namespace UmbracoHeadlessBFF.SharedModules.Common.ServiceDiscovery;

public static class ServiceDiscoveryExtensions
{
    extension(IConfiguration configuration)
    {
        public string? GetServiceEndpoint(string serviceName, string endpointName,
            int index = 0)
        {
            return configuration[$"services:{serviceName}:{endpointName}:{index}"];
        }

        public string[]? GetServiceEndpoints(string serviceName, string endpointName)
        {
            var endpoints = configuration.GetSection($"services:{serviceName}:{endpointName}");

            return endpoints.Get<string[]>();
        }
    }
}
