using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class SpotlightMapper : IComponentMapper
{
    private readonly ResponsiveImageMapper _responsiveImageMapper;
    private readonly LinkMapper _linkMapper;

    public SpotlightMapper(ResponsiveImageMapper responsiveImageMapper, LinkMapper linkMapper)
    {
        _responsiveImageMapper = responsiveImageMapper;
        _linkMapper = linkMapper;
    }

    public bool CanMap(string type)
    {
        return type == DeliveryApiConstants.ElementTypes.ApiSpotlight;
    }

    public async Task<IComponent> Map(IApiElement model)
    {
        var apiModel = model.ToOrThrow<ApiSpotlight>();

        var media = apiModel.Properties.Media?.Items
            .Select(x => x.Content)
            .OfType<ApiResponsiveImage>()
            .FirstOrDefault();

        var cta = apiModel.Properties.Cta?.FirstOrDefault();

        return new Spotlight
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Heading = apiModel.Properties.Heading,
            HeadingSize = apiModel.Properties.HeadingSize,
            Description = apiModel.Properties.Description?.Markup,
            Media = media is not null ? await _responsiveImageMapper.Map(media) : null,
            Cta = cta is not null ? await _linkMapper.Map(cta) : null
        };
    }
}
