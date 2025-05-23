using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Components;

internal sealed class CarouselMapper : IComponentMapper
{
    private readonly ICardMapper _cardMapper;

    public CarouselMapper(ICardMapper cardMapper)
    {
        _cardMapper = cardMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiCarousel;

    public async Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        if (model is not ApiCarousel apiModel)
        {
            return null;
        }

        return new Carousel
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Heading = apiModel.Properties.Heading,
            HeadingSize = apiModel.Properties.HeadingSize,
            SubHeading = apiModel.Properties.SubHeading,
            Cards = await _cardMapper.Map(apiModel.Properties.Cards?.Items.Select(x => x.Content) ?? [])
        };
    }
}
