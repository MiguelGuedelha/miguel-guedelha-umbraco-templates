using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using UmbracoHeadlessBFF.SharedModules.Common.Versioning;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace UmbracoHeadlessBFF.SharedModules.Common.Caching;

public static class CachingConfiguration
{
    extension(WebApplicationBuilder builder)
    {
        public void AddCachingSharedModule(
            string? cacheName = null,
            bool versioned = false,
            Action<FusionCacheOptions>? configureOptions = null,
            Action<FusionCacheEntryOptions>? configureEntryOptions = null,
            Action<JsonSerializerOptions>? configureJsonSerializerOptions = null)
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

            var cacheBuilder = builder.Services
                .AddFusionCache(cacheName ?? FusionCacheOptions.DefaultCacheName);

            if (versioned)
            {
                cacheBuilder.WithCacheKeyPrefix($"{cacheName ?? FusionCacheOptions.DefaultCacheName}:{AssemblyVersionExtensions.GetVersion()}:");
            }
            else
            {
                cacheBuilder.WithCacheKeyPrefixByCacheName();
            }

            if (!defaultCachingOptions.Enabled)
            {
                cacheBuilder.WithNullImplementation();
                return;
            }

            builder.Services.AddMemoryCache();
            builder.AddRedisDistributedCache(CachingConstants.ConnectionStringName);

            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            configureJsonSerializerOptions?.Invoke(jsonOptions);

            cacheBuilder
                .WithOptions(o =>
                {
                    //Backs off the Distributed Cache if having issues
                    o.DistributedCacheCircuitBreakerDuration =
                        TimeSpan.FromSeconds(defaultCachingOptions.DistributedCacheCircuitBreakerDuration);

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

                    o.DistributedCacheSoftTimeout =
                        TimeSpan.FromMilliseconds(defaultCachingOptions.DistributedCacheSoftTimeoutMs);
                    o.DistributedCacheHardTimeout = TimeSpan.FromSeconds(defaultCachingOptions.DistributedCacheHardTimeout);
                    o.AllowBackgroundDistributedCacheOperations =
                        defaultCachingOptions.AllowBackgroundDistributedCacheOperations;
                    o.AllowBackgroundBackplaneOperations = defaultCachingOptions.AllowBackgroundBackplaneOperations;

                    o.JitterMaxDuration = TimeSpan.FromSeconds(defaultCachingOptions.JitterMaxDuration);

                    o.EagerRefreshThreshold = defaultCachingOptions.EagerRefreshThreshold;

                    configureEntryOptions?.Invoke(o);
                })
                .WithSerializer(new FusionCacheSystemTextJsonSerializer(jsonOptions))
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
}
