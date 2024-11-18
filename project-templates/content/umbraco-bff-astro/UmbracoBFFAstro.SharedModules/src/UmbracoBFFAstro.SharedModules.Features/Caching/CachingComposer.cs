using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Backplane.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace UmbracoBFFAstro.SharedModules.Features.Caching;

public static class CachingConfiguration
{
    public static void AddCaching(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddMemoryCache();
        builder.Services.AddFusionCache()
            .WithOptions(o =>
            {
                //Backs off the Distributed Cache if having issues
                o.DistributedCacheCircuitBreakerDuration = TimeSpan.FromSeconds(10);

                //Log Levels (examples)
                o.FailSafeActivationLogLevel = LogLevel.Debug;
                o.SerializationErrorsLogLevel = LogLevel.Warning;
                o.DistributedCacheSyntheticTimeoutsLogLevel = LogLevel.Debug;
                o.DistributedCacheErrorsLogLevel = LogLevel.Error;
                o.FactorySyntheticTimeoutsLogLevel = LogLevel.Debug;
                o.FactoryErrorsLogLevel = LogLevel.Error;
            })
            .WithDefaultEntryOptions(o =>
            {
                //General
                o.Duration = TimeSpan.FromMinutes(15);

                //Failsafe
                o.IsFailSafeEnabled = true;
                o.FailSafeMaxDuration = TimeSpan.FromMinutes(5);
                o.FailSafeThrottleDuration = TimeSpan.FromMinutes(30);

                //Factory Timeouts
                o.FactorySoftTimeout = TimeSpan.FromMilliseconds(300);
                o.FactoryHardTimeout = TimeSpan.FromSeconds(5);

                //Distributed Cache Options
                o.DistributedCacheSoftTimeout = TimeSpan.FromMilliseconds(500);
                o.DistributedCacheHardTimeout = TimeSpan.FromSeconds(2);
                o.AllowBackgroundDistributedCacheOperations = true;
                o.AllowBackgroundBackplaneOperations  = true;

                //Jitter
                o.JitterMaxDuration = TimeSpan.FromSeconds(10);
            })
            .WithSerializer(new FusionCacheNewtonsoftJsonSerializer())
            .WithDistributedCache(new RedisCache(new RedisCacheOptions { Configuration = configuration.GetConnectionString("cache")}))
            .WithBackplane(new RedisBackplane(new RedisBackplaneOptions { Configuration = configuration.GetConnectionString("cache")}));

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddFusionCacheInstrumentation())
            .WithMetrics(metrics => metrics.AddFusionCacheInstrumentation());
    }
}
