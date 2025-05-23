using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages.Shared;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

internal sealed class HomeMapper : BasePageMapper, IPageMapper
{
    public HomeMapper(ISeoMapper seoMapper, SiteResolutionContext siteResolutionContext,
        SiteResolutionService siteResolutionService, IEnumerable<ILayoutMapper> layoutMappers)
        : base(seoMapper, siteResolutionContext, siteResolutionService, layoutMappers)
    {
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ContentTypes.ApiHome;

    public async Task<IPage?> Map(IApiContent model)
    {
        if (model is not ApiHome apiModel)
        {
            return null;
        }

        return new Home
        {
            Id = model.Id,
            ContentType = model.ContentType,
            Content = new()
            {
                MainContent = await MapMainContent(apiModel),
                AdditionalProperties = new()
            },
            Context = await MapPageContext(apiModel.Properties)
        };
    }
}
