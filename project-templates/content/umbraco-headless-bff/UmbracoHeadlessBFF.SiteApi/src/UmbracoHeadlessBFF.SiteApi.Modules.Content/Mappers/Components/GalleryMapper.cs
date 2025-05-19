using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Gallery;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class GalleryMapper : IComponentMapper
{
    private readonly IMapper<IEnumerable<ApiCard>, IReadOnlyCollection<Card>> _cardsMapper;

    public GalleryMapper(IMapper<IEnumerable<ApiCard>, IReadOnlyCollection<Card>> cardsMapper)
    {
        _cardsMapper = cardsMapper;
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
            Items = await _cardsMapper.Map(apiModel.Properties.Items?.Items.Select(x => x.Content) ?? []),
            CardsPerRow = apiSettings.Properties.CardsPerRow
        };
    }
}
