using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Shared;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

internal sealed class SiteSearchMapper : BasePageMapper, IPageMapper
{
    public SiteSearchMapper(ISeoMapper seoMapper, SiteResolutionContext siteResolutionContext,
        SiteResolutionService siteResolutionService, IEnumerable<ILayoutMapper> layoutMappers)
        : base(seoMapper, siteResolutionContext, siteResolutionService, layoutMappers)
    {
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ContentTypes.ApiSiteSearch;

    public async Task<IPage?> Map(IApiContent model)
    {
        if (model is not ApiSiteSearch apiModel)
        {
            return null;
        }

        return new SiteSearch
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Context = await MapPageContext(apiModel.Properties)
        };
    }
}
