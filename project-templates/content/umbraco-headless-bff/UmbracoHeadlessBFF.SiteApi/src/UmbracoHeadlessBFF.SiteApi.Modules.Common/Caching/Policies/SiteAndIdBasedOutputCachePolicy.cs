using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Primitives;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;

public sealed class SiteAndIdBasedOutputCachePolicy : SiteApiOutputCachePolicyBase, IOutputCachePolicy
{
    public const string PolicyName = "SiteAndIdBasedOutputCachePolicy";

    public static readonly SiteAndIdBasedOutputCachePolicy Instance = new();

    private SiteAndIdBasedOutputCachePolicy()
    {
    }

    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var canCacheBySite = CanCacheBySite(context, out var siteId);

        context.EnableOutputCaching = true;
        context.AllowCacheLookup = canCacheBySite;
        context.AllowCacheStorage = canCacheBySite;
        context.AllowLocking = true;
        context.ResponseExpirationTimeSpan = TimeSpan.FromSeconds(60);

        if (!canCacheBySite)
        {
            return ValueTask.CompletedTask;
        }

        context.CacheVaryByRules.VaryByValues["siteId"] = siteId!;
        context.CacheVaryByRules.QueryKeys = new StringValues("id");

        return ValueTask.CompletedTask;
    }

    public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        ServeResponseBaseAsync(context);
        return ValueTask.CompletedTask;
    }
}
