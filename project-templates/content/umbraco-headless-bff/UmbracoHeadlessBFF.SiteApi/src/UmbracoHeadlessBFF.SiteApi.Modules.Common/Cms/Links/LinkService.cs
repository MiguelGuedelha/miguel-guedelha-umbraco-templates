using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Clients;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Contracts;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Links;

public sealed class LinkService
{
    private readonly ILinksApi _linksApi;
    private readonly SiteResolutionContext _siteResolutionContext;

    public LinkService(ILinksApi linksApi, SiteResolutionContext siteResolutionContext)
    {
        _linksApi = linksApi;
        _siteResolutionContext = siteResolutionContext;
    }

    public async Task<Link?> ResolveLink(Guid id)
    {
        var response = await _linksApi.GetLink(id, _siteResolutionContext.Site.CultureInfo, _siteResolutionContext.IsPreview);

        return response.Content;
    }
}
