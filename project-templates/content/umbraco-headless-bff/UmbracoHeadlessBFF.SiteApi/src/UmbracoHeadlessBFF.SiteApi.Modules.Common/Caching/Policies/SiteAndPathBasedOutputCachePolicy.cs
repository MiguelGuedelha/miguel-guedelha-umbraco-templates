using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
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
        context.EnableOutputCaching = true;
        context.AllowLocking = true;

        var siteResolutionContext = context.HttpContext.RequestServices.GetService<SiteResolutionContext>();

        if (siteResolutionContext is null)
        {
            context.EnableOutputCaching = false;
            context.AllowCacheLookup = false;
            context.AllowCacheStorage = false;
            return ValueTask.CompletedTask;
        }

        var canCacheBySitePath = CanCacheBySitePath(siteResolutionContext, out var sitePath);
        var canCacheBySite = CanCacheBase(context, siteResolutionContext, out var siteId);

        var canCache = canCacheBySitePath && canCacheBySite;

        context.EnableOutputCaching = true;
        context.AllowCacheLookup = canCache;
        context.AllowCacheStorage = canCache;
        context.AllowLocking = true;

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
        try
        {
            var siteResolutionContext = context.HttpContext.RequestServices.GetRequiredService<SiteResolutionContext>();

            context.Tags.Add(siteResolutionContext.Site.DictionaryId.ToString());
            context.Tags.Add(siteResolutionContext.Site.SiteSettingsId.ToString());
            context.Tags.Add(CachingConstants.SiteApi.Tags.Sitemaps);
            context.Tags.Add(CachingConstants.SiteApi.Tags.Robots);
        }
        catch
        {
            context.AllowCacheStorage = false;
            throw;
        }
        return ValueTask.CompletedTask;
    }

    private static bool CanCacheBySitePath(SiteResolutionContext siteResolutionContext, [NotNullWhen(true)] out string? sitePath)
    {
        sitePath = null;
        try
        {
            sitePath = siteResolutionContext.Path;
            return !string.IsNullOrWhiteSpace(sitePath);
        }
        catch
        {
            return false;
        }
    }
}
