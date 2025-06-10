using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
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
    public static void AddCachingSharedModule(this WebApplicationBuilder builder, string? cachePrefix = null, Action<FusionCacheOptions>? configureOptions = null, Action<FusionCacheEntryOptions>? configureEntryOptions = null)
    {
        var section = builder.Configuration
            .GetSection(DefaultCachingOptions.SectionName);

        builder.Services.Configure<DefaultCachingOptions>(section);

        var defaultCachingOptions = section
            .Get<DefaultCachingOptions>();

        if (defaultCachingOptions is null)
        {
            throw new ArgumentException("Caching options can't be parsed");
        }

        if (!defaultCachingOptions.Enabled)
        {
            builder.Services.AddFusionCache().WithNullImplementation();
            return;
        }

        builder.Services.AddMemoryCache();

        builder.AddRedisDistributedCache(CachingConstants.ConnectionStringName);

        builder.Services.AddFusionCache()
            .WithOptions(o =>
            {
                if (cachePrefix is not null)
                {
                    var cleanPrefix = cachePrefix.TrimEnd(':');
                    var cachePrefixWithSeparator = $"{cleanPrefix}:";
                    o.CacheKeyPrefix = cachePrefixWithSeparator;
                    o.BackplaneChannelPrefix = cleanPrefix;
                }

                //Backs off the Distributed Cache if having issues
                o.DistributedCacheCircuitBreakerDuration = TimeSpan.FromSeconds(defaultCachingOptions.DistributedCacheCircuitBreakerDuration);

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

                configureOptions?.Invoke(o);
            })
            .WithDefaultEntryOptions(o =>
            {
                o.Duration = TimeSpan.FromSeconds(defaultCachingOptions.Duration);

                o.IsFailSafeEnabled = defaultCachingOptions.FailSafeIsEnabled;
                o.FailSafeMaxDuration = TimeSpan.FromSeconds(defaultCachingOptions.FailSafeMaxDuration);
                o.FailSafeThrottleDuration = TimeSpan.FromSeconds(defaultCachingOptions.FailSafeThrottleDuration);

                o.FactorySoftTimeout = TimeSpan.FromMilliseconds(defaultCachingOptions.FactorySoftTimeoutMs);
                o.FactoryHardTimeout = TimeSpan.FromSeconds(defaultCachingOptions.FactoryHardTimeout);

                o.DistributedCacheSoftTimeout = TimeSpan.FromMilliseconds(defaultCachingOptions.DistributedCacheSoftTimeoutMs);
                o.DistributedCacheHardTimeout = TimeSpan.FromSeconds(defaultCachingOptions.DistributedCacheHardTimeout);
                o.AllowBackgroundDistributedCacheOperations = defaultCachingOptions.AllowBackgroundDistributedCacheOperations;
                o.AllowBackgroundBackplaneOperations = defaultCachingOptions.AllowBackgroundBackplaneOperations;

                o.JitterMaxDuration = TimeSpan.FromSeconds(defaultCachingOptions.JitterMaxDuration);

                o.EagerRefreshThreshold = defaultCachingOptions.EagerRefreshThreshold;

                configureEntryOptions?.Invoke(o);
            })
            .WithSerializer(new FusionCacheNewtonsoftJsonSerializer())
            .WithRegisteredDistributedCache()
            .WithStackExchangeRedisBackplane(o =>
            {
                o.Configuration = builder.Configuration.GetConnectionString(CachingConstants.ConnectionStringName);
            });

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddFusionCacheInstrumentation())
            .WithMetrics(metrics => metrics.AddFusionCacheInstrumentation());
    }
}
