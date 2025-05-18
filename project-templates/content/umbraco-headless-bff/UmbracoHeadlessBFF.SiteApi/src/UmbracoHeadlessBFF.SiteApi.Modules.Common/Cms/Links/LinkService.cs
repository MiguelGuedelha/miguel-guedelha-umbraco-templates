using UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Contracts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.Links;

public class LinkService
{
    private readonly ILinksApi _linksApi;

    public LinkService(ILinksApi linksApi)
    {
        _linksApi = linksApi;
    }

    public Task<Link> ResolveLink(Guid id, string culture)
    {

    }
}
