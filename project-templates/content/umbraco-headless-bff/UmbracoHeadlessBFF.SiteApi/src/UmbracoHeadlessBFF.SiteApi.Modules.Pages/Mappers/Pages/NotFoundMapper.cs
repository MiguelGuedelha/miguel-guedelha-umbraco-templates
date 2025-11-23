using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Pages;

internal sealed class NotFoundMapper : IPageMapper
{
    private readonly BasePageMapper _basePageMapper;

    public NotFoundMapper(BasePageMapper basePageMapper)
    {
        _basePageMapper = basePageMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ContentTypes.ApiNotFound;

    public async Task<IPage?> Map(IApiContent? model)
    {
        if (model is not ApiNotFound apiModel)
        {
            return null;
        }

        return new NotFound
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
