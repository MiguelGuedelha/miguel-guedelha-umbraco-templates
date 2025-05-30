using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Pages;

internal sealed class HomeMapper : IPageMapper
{
    private readonly BasePageMapper _basePageMapper;

    public HomeMapper(BasePageMapper basePageMapper)
    {
        _basePageMapper = basePageMapper;
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
                MainContent = await _basePageMapper.MapMainContent(apiModel),
                AdditionalProperties = new()
            },
            Context = await _basePageMapper.MapPageContext(apiModel)
        };
    }
}
