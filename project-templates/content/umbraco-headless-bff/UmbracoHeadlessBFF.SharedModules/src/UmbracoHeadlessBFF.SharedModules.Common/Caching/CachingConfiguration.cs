using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace UmbracoHeadlessBFF.SharedModules.Common.Caching;

public static class CachingConfiguration
{
    public static void AddCachingSharedModule(this WebApplicationBuilder builder, string? cachePrefix = null, Action<FusionCacheEntryOptions>? configure = null)
    {
        builder.Services.AddMemoryCache();

        builder.AddRedisDistributedCache(CachingConstants.ConnectionStringName);

        builder.Services.AddFusionCacheStackExchangeRedisBackplane();

        builder.Services.AddFusionCache()
            .WithOptions(o =>
            {
                if (cachePrefix is not null)
                {
                    o.CacheKeyPrefix = cachePrefix;
                }

                //Backs off the Distributed Cache if having issues
                o.DistributedCacheCircuitBreakerDuration = TimeSpan.FromSeconds(10);

                //Log Levels (examples)
                o.FailSafeActivationLogLevel = LogLevel.Debug;
                o.SerializationErrorsLogLevel = LogLevel.Warning;
                o.DistributedCacheSyntheticTimeoutsLogLevel = LogLevel.Debug;
                o.DistributedCacheErrorsLogLevel = LogLevel.Error;
                o.FactorySyntheticTimeoutsLogLevel = LogLevel.Debug;
                o.FactoryErrorsLogLevel = LogLevel.Error;
                o.BackplaneErrorsLogLevel = LogLevel.Warning;
                o.EventHandlingErrorsLogLevel = LogLevel.Warning;
                o.IncoherentOptionsNormalizationLogLevel = LogLevel.Error;
            })
            .WithDefaultEntryOptions(o =>
            {
                //General
                o.Duration = TimeSpan.FromMinutes(15);

                //Failsafe
                o.IsFailSafeEnabled = true;
                o.FailSafeMaxDuration = TimeSpan.FromHours(2);
                o.FailSafeThrottleDuration = TimeSpan.FromMinutes(5);

                //Factory Timeouts
                o.FactorySoftTimeout = TimeSpan.FromMilliseconds(300);
                o.FactoryHardTimeout = TimeSpan.FromSeconds(5);

                //Distributed Cache Options
                o.DistributedCacheSoftTimeout = TimeSpan.FromMilliseconds(100);
                o.DistributedCacheHardTimeout = TimeSpan.FromSeconds(5);
                o.AllowBackgroundDistributedCacheOperations = true;
                o.AllowBackgroundBackplaneOperations = true;

                //Jitter
                o.JitterMaxDuration = TimeSpan.FromSeconds(10);

                configure?.Invoke(o);
            })
            .WithSerializer(new FusionCacheNewtonsoftJsonSerializer())
            .WithRegisteredDistributedCache()
            .WithRegisteredBackplane();

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddFusionCacheInstrumentation())
            .WithMetrics(metrics => metrics.AddFusionCacheInstrumentation());
    }
}
