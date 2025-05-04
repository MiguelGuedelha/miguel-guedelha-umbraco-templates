using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class ServiceDiscoveryExtensions
{
    public static string? GetServiceEndpoint(this IConfiguration configuration, string serviceName, string endpointName,
        int index = 0)
    {
        return configuration[$"services:{serviceName}:{endpointName}:{index}"];
    }

    public static string[]? GetServiceEndpoints(this IConfiguration configuration, string serviceName, string endpointName)
    {
        var endpoints = configuration.GetSection($"services:{serviceName}:{endpointName}");

        return endpoints.Get<string[]>();
    }
}
