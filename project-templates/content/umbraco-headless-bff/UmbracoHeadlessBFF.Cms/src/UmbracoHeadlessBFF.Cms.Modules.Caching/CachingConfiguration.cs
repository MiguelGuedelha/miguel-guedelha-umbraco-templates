using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace UmbracoHeadlessBFF.Cms.Modules.Caching;

public static class CachingConfiguration
{
    public static void AddCachingModule(this WebApplicationBuilder builder)
    {
        builder.AddAzureServiceBusClient("ServiceBus");
    }
}
