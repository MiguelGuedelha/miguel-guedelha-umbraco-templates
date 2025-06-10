using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Cms.Links;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Links;

public sealed class LinkService
{
    private readonly ILinksApi _linksApi;
    private readonly SiteResolutionContext _siteResolutionContext;
    private readonly IFusionCache _fusionCache;
    private readonly IOptionsSnapshot<DefaultCachingOptions> _defaultCachingOptions;

    public LinkService(ILinksApi linksApi,
        SiteResolutionContext siteResolutionContext,
        IFusionCache fusionCache,
        IOptionsSnapshot<DefaultCachingOptions> defaultCachingOptions)
    {
        _linksApi = linksApi;
        _siteResolutionContext = siteResolutionContext;
        _fusionCache = fusionCache;
        _defaultCachingOptions = defaultCachingOptions;
    }

    public async Task<Link?> ResolveLink(Guid id)
    {
        if (_siteResolutionContext.IsPreview)
        {
            var response = await _linksApi.GetLink(id, _siteResolutionContext.Site.CultureInfo, true);
            return response.Content;
        }


        var data = await _fusionCache.GetOrSetAsync<Link?>(
            $"links:id:{id}:culture:{_siteResolutionContext.Site.CultureInfo}",
            async (ctx, ct) =>
            {
                var response = await _linksApi.GetLink(id, _siteResolutionContext.Site.CultureInfo, _siteResolutionContext.IsPreview, ct);

                if (response is { IsSuccessful: true, Content: not null })
                {
                    return response.Content;
                }

                ctx.Options.Duration = TimeSpan.FromSeconds(_defaultCachingOptions.Value.NullDuration);

                return null;
            });

        return data;
    }
}
