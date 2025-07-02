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

        //Use fusion cache (and by extension all its perks) under the hood for the umbraco cache
        // builder.Services
        //     .AddFusionCache("umbraco")
        //     .WithCacheKeyPrefixByCacheName()
        //     .WithDefaultEntryOptions(o =>
        //     {
        //         o.IsFailSafeEnabled = true;
        //     })
        //     .WithSerializer(new FusionCacheSystemTextJsonSerializer())
        //     .WithRegisteredDistributedCache()
        //     .WithStackExchangeRedisBackplane(o =>
        //     {
        //         o.Configuration = builder.Configuration.GetConnectionString(CachingConstants.ConnectionStringName);
        //     })
        //     .AsHybridCache();
        //
        // builder.Services.AddOpenTelemetry()
        //     .WithTracing(tracing => tracing.AddFusionCacheInstrumentation())
        //     .WithMetrics(metrics => metrics.AddFusionCacheInstrumentation());
    }
}
