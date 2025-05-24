using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Pages;

internal sealed class StandardContentMapper : IPageMapper
{
    private readonly BasePageMapper _basePageMapper;

    public StandardContentMapper(BasePageMapper basePageMapper)
    {
        _basePageMapper = basePageMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ContentTypes.ApiStandardContentPage;

    public async Task<IPage?> Map(IApiContent model)
    {
        if (model is not ApiStandardContentPage apiModel)
        {
            return null;
        }

        return new Home
        {
            Id = model.Id,
            ContentType = model.ContentType,
            Content = new()
            {
                MainContent = await _basePageMapper.MapMainContent(apiModel),
                AdditionalProperties = new()
            },
            Context = await _basePageMapper.MapPageContext(apiModel)
        };
    }
}
