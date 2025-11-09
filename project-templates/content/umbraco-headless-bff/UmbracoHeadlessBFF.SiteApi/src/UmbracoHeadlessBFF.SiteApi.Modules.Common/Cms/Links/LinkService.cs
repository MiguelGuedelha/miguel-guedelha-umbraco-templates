using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Cms.Links;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using ZiggyCreatures.Caching.Fusion;
using CachingConstants = UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.CachingConstants;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Links;

public sealed class LinkService
{
    private readonly ILinksApi _linksApi;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly IFusionCache _fusionCache;
    private readonly DefaultCachingOptions _defaultCachingOptions;

    public LinkService(ILinksApi linksApi,
        SiteResolutionContext siteResolutionContext,
        IFusionCacheProvider fusionCacheProvider,
        IOptionsSnapshot<DefaultCachingOptions> defaultCachingOptions)
    {
        _linksApi = linksApi;
        _siteResolutionContext = siteResolutionContext;
        _fusionCache = fusionCacheProvider.GetCache(CachingConstants.SiteApiCacheName);
        _defaultCachingOptions = defaultCachingOptions.Value;
    }

    public async Task<Link?> ResolveLink(Guid id)
    {
        var culture =  _siteResolutionContext.Site.CultureInfo;

        if (_siteResolutionContext.IsPreview)
        {
            var response = await _linksApi.GetLink(id, culture, true);
            return response.Content;
        }

        var data = await _fusionCache.GetOrSetAsync<Link?>(
            $"Region:{CachingRegionConstants.Links}:{id}-{culture}",
            async (ctx, ct) =>
            {
                var response = await _linksApi.GetLink(id, culture, false, ct);

                if (response is { IsSuccessful: true, Content: not null })
                {
                    return response.Content;
                }

                ctx.Options.SetAllDurations(TimeSpan.FromSeconds(_defaultCachingOptions.NullDuration));

                return null;
            },
            tags: [CachingTagConstants.Links, id.ToString(), culture]);

        return data;
    }
}
