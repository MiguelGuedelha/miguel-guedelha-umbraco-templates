using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;

public sealed class SiteBasedOutputCachePolicy : SiteApiOutputCachePolicyBase, IOutputCachePolicy
{
    public const string PolicyName = "SiteBasedOutputCachePolicy";

    public static readonly SiteBasedOutputCachePolicy Instance = new();

    private SiteBasedOutputCachePolicy()
    {
    }

    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var canCache = CanCacheBySite(context, out var siteId);
        context.EnableOutputCaching = true;
        context.AllowCacheLookup = canCache;
        context.AllowCacheStorage = canCache;
        context.AllowLocking = true;

        if (canCache)
        {
            context.CacheVaryByRules.VaryByValues["siteId"] = siteId!;
        }

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
