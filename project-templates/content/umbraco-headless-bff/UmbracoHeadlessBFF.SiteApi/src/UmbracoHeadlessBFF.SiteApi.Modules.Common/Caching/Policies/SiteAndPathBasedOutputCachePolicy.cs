using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;

public sealed class SiteAndPathBasedOutputCachePolicy : SiteApiOutputCachePolicyBase, IOutputCachePolicy
{
    public const string PolicyName = "SiteBasedOutputCachePolicy";

    public static readonly SiteAndPathBasedOutputCachePolicy Instance = new();

    private SiteAndPathBasedOutputCachePolicy()
    {
    }

    public ValueTask CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var canCache = CanCacheByPath(context, out var varyParams);
        context.EnableOutputCaching = true;
        context.AllowCacheLookup = canCache;
        context.AllowCacheStorage = canCache;
        context.AllowLocking = true;

        if (!canCache)
        {
            return ValueTask.CompletedTask;
        }

        context.CacheVaryByRules.VaryByValues["siteId"] = varyParams!.Value.SiteId;
        context.CacheVaryByRules.VaryByValues["sitePath"] = varyParams.Value.SitePath;

        return ValueTask.CompletedTask;
    }

    public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask ServeResponseAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        var response = context.HttpContext.Response;

        // Verify existence of cookie headers
        if (!StringValues.IsNullOrEmpty(response.Headers.SetCookie))
        {
            context.AllowCacheStorage = false;
            return ValueTask.CompletedTask;
        }

        // Check response code
        if (response.StatusCode is StatusCodes.Status200OK)
        {
            return ValueTask.CompletedTask;
        }

        context.AllowCacheStorage = false;
        return ValueTask.CompletedTask;
    }

    private static bool CanCacheByPath(OutputCacheContext context, [NotNullWhen(true)] out (string SiteId, string SitePath)? siteCacheParams)
    {
        siteCacheParams = null;
        var canCacheBySite = CanCacheBySite(context, out var siteId);

        if (!canCacheBySite)
        {
            return false;
        }

        var siteContext = context.HttpContext.RequestServices.GetRequiredService<SiteResolutionContext>();

        try
        {
            var sitePath = siteContext.Path;
            if (string.IsNullOrWhiteSpace(sitePath))
            {
                return false;
            }

            siteCacheParams = (siteId!, sitePath);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
