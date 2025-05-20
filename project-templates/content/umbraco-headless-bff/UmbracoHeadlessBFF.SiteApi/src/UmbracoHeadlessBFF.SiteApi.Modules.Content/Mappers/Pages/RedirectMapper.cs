using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Shared;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

internal sealed class RedirectMapper : BasePageMapper, IPageMapper
{
    private static readonly string[] s_redirectPageTypes = [
        DeliveryApiConstants.ContentTypes.ApiRedirect,
        DeliveryApiConstants.ContentTypes.ApiBlogRepository,
        DeliveryApiConstants.ContentTypes.ApiBlogYear,
        DeliveryApiConstants.ContentTypes.ApiBlogMonth
    ];

    private readonly ILinkMapper _linkMapper;

    public RedirectMapper(ISeoMapper seoMapper, SiteResolutionContext siteResolutionContext,
        SiteResolutionService siteResolutionService, IEnumerable<ILayoutMapper> layoutMappers,
        ILinkMapper linkMapper)
        : base(seoMapper, siteResolutionContext, siteResolutionService, layoutMappers)
    {
        _linkMapper = linkMapper;
    }

    public bool CanMap(string type) => s_redirectPageTypes.Contains(type);

    public async Task<IPage?> Map(IApiContent model)
    {
        if (model is not IApiContent<IRedirectSettingsProperties> apiModel)
        {
            return null;
        }

        var link = apiModel.Properties.RedirectLink?.FirstOrDefault();

        var redirectUrl = link is not null ? (await _linkMapper.Map(link))?.Href : null;

        if (redirectUrl is not null)
        {
            return new Redirect
            {
                Id = apiModel.Id,
                ContentType = apiModel.ContentType,
                Context = await MapPageContext(apiModel),
                RedirectUrl = redirectUrl
            };
        }

        //TODO: resolve redirect link based on fallback direction
        //TODO: create a redirects module in the CMS and an api method
        //TODO: make the redirects be a pre-mapping or even pre-fetching thing in the content endpoint
        //  and get rid of redirect mapper and the redirect related stuff/mappings in the site api

        return new Redirect
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Context = await MapPageContext(apiModel),
            RedirectUrl = redirectUrl
        };
    }
}
