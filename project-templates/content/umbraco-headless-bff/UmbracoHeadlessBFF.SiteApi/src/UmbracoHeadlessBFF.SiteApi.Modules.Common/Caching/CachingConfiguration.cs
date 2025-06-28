using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NeueccMessagePack;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;

public static class CachingConfiguration
{
    public static void AddCachingCommonModule(this WebApplicationBuilder builder, string? cacheName = FusionCacheOptions.DefaultCacheName)
    {
        var outputCacheName = $"{cacheName}OutputCache";
        builder.Services.AddFusionCache(outputCacheName)
            .WithCacheKeyPrefixByCacheName()
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

        builder.Services.AddFusionOutputCache(o =>
        {
            o.CacheName = outputCacheName;
        });

        builder.Services.AddOutputCache(o =>
        {
            o.AddPolicy(SiteBasedOutputCachePolicy.PolicyName, SiteBasedOutputCachePolicy.Instance);
            o.AddPolicy(SiteAndPathBasedOutputCachePolicy.PolicyName, SiteAndPathBasedOutputCachePolicy.Instance);
            o.AddPolicy(SiteAndIdBasedOutputCachePolicy.PolicyName, SiteAndIdBasedOutputCachePolicy.Instance);
        });
    }
}
