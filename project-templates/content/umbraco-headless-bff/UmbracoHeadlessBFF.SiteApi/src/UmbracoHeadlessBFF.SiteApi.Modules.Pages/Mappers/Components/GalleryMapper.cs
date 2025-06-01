using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Components;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Components;

internal sealed class GalleryMapper : IComponentMapper
{
    private readonly ICardMapper _cardMapper;

    public GalleryMapper(ICardMapper cardMapper)
    {
        _cardMapper = cardMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiGallery;

    public async Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        if (model is not ApiGallery apiModel || settings is not ApiGallerySettings apiSettings)
        {
            return null;
        }

        return new Gallery
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Heading = apiModel.Properties.Heading,
            HeadingSize = apiModel.Properties.HeadingSize,
            SubHeading = apiModel.Properties.SubHeading,
            Items = await _cardMapper.Map(apiModel.Properties.Items?.Items.Select(x => x.Content) ?? []),
            CardsPerRow = apiSettings.Properties.CardsPerRow
        };
    }
}
