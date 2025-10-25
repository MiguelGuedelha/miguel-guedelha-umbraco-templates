using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Versioning;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NeueccMessagePack;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;

public static class CachingConfiguration
{
    public static void AddCachingCommonModule(this WebApplicationBuilder builder, bool versioned = false)
    {
        var cacheBuilder = builder.Services.AddFusionCache(CachingConstants.SiteApiOutputCacheName)
            .WithDefaultEntryOptions(o =>
            {
                o.IsFailSafeEnabled = true;
            })
            .WithSerializer(new FusionCacheNeueccMessagePackSerializer())
            .WithRegisteredDistributedCache()
            .WithStackExchangeRedisBackplane(o =>
            {
                o.Configuration = builder.Configuration.GetConnectionString(SharedModules.Common.Caching.CachingConstants.ConnectionStringName);
            });

        if (versioned)
        {
            cacheBuilder.WithCacheKeyPrefix($"{CachingConstants.SiteApiOutputCacheName}:{AssemblyVersionExtensions.GetVersion()}:");
        }
        else
        {
            cacheBuilder.WithCacheKeyPrefix($"{CachingConstants.SiteApiOutputCacheName}:");
        }

        builder.Services.AddFusionOutputCache(o =>
        {
            o.CacheName = CachingConstants.SiteApiOutputCacheName;
        });

        builder.Services.AddOutputCache(o =>
        {
            o.AddPolicy(SiteBasedOutputCachePolicy.PolicyName, SiteBasedOutputCachePolicy.Instance);
            o.AddPolicy(SiteAndPathBasedOutputCachePolicy.PolicyName, SiteAndPathBasedOutputCachePolicy.Instance);
            o.AddPolicy(SiteAndIdBasedOutputCachePolicy.PolicyName, SiteAndIdBasedOutputCachePolicy.Instance);
        });
    }
}
