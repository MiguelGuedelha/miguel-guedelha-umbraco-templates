using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.Cms.Modules.Caching;

public static class CachingConfiguration
{
    public static void AddCachingModule(this WebApplicationBuilder builder)
    {
        var isSiteApiCachingEnabled = builder.Configuration
            .GetSection("Caching:SiteApiCacheEnabled")
            .Get<bool>();

        var cacheBuilder = builder.Services.AddFusionCache(CachingConstants.SiteApiCacheName)
            .WithCacheKeyPrefix();

        if (!isSiteApiCachingEnabled)
        {
            cacheBuilder.WithNullImplementation();
            return;
        }

        cacheBuilder.WithStackExchangeRedisBackplane(o =>
        {
            o.Configuration = builder.Configuration.GetConnectionString(CachingConstants.ConnectionStringName);
        });
    }
}
