using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

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

        var canCacheBySite = CanCacheBase(context, siteResolutionContext, out var siteId);

        context.EnableOutputCaching = true;
        context.AllowCacheLookup = canCacheBySite;
        context.AllowCacheStorage = canCacheBySite;
        context.AllowLocking = true;

        if (!canCacheBySite)
        {
            return ValueTask.CompletedTask;
        }

        context.CacheVaryByRules.VaryByValues["siteId"] = siteId!;
        context.CacheVaryByRules.QueryKeys = new("id");

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

            context.Tags.Add(siteResolutionContext.PageId.ToString());
            context.Tags.Add(siteResolutionContext.Site.SiteSettingsId.ToString());
            context.Tags.Add(siteResolutionContext.Site.DictionaryId.ToString());
        }
        catch
        {
            context.AllowCacheStorage = false;
            throw;
        }
        return ValueTask.CompletedTask;
    }
}
