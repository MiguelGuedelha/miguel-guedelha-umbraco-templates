using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;

public sealed class SiteAndPathBasedOutputCachePolicy : SiteApiOutputCachePolicyBase, IOutputCachePolicy
{
    public const string PolicyName = "SiteAndPathBasedOutputCachePolicy";

    public static readonly SiteAndPathBasedOutputCachePolicy Instance = new();

    private SiteAndPathBasedOutputCachePolicy()
    {
    }

    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var canCacheBySitePath = CanCacheBySitePath(context, out var sitePath);
        var canCacheBySite = CanCacheBySite(context, out var siteId);

        var canCache = canCacheBySitePath && canCacheBySite;

        context.EnableOutputCaching = true;
        context.AllowCacheLookup = canCache;
        context.AllowCacheStorage = canCache;
        context.AllowLocking = true;
        context.ResponseExpirationTimeSpan = TimeSpan.FromSeconds(60);

        if (!canCache)
        {
            return ValueTask.CompletedTask;
        }

        context.CacheVaryByRules.VaryByValues["siteId"] = siteId!;
        context.CacheVaryByRules.VaryByValues["sitePath"] = sitePath!;

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

    private static bool CanCacheBySitePath(OutputCacheContext context, [NotNullWhen(true)] out string? sitePath)
    {
        sitePath = null;

        var siteContext = context.HttpContext.RequestServices.GetRequiredService<SiteResolutionContext>();

        try
        {
            sitePath = siteContext.Path;
            return !string.IsNullOrWhiteSpace(sitePath);
        }
        catch
        {
            return false;
        }
    }
}
