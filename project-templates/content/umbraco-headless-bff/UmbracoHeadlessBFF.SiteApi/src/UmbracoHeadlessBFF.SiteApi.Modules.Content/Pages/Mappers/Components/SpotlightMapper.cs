using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Components;

internal sealed class SpotlightMapper : IComponentMapper
{
    private readonly ILinkMapper _linkMapper;
    private readonly IMediaBlockMapper _mediaBlockMapper;

    public SpotlightMapper(ILinkMapper linkMapper, IMediaBlockMapper mediaBlockMapper)
    {
        _linkMapper = linkMapper;
        _mediaBlockMapper = mediaBlockMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiSpotlight;

    public async Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        if (model is not ApiSpotlight apiModel)
        {
            return null;
        }

        var media = apiModel.Properties.Media?.Items
            .Select(x => x.Content)
            .FirstOrDefault();

        var cta = apiModel.Properties.Cta?.FirstOrDefault();

        return new Spotlight
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Heading = apiModel.Properties.Heading,
            HeadingSize = apiModel.Properties.HeadingSize,
            Description = apiModel.Properties.Description?.Markup,
            Media = media is not null ? await _mediaBlockMapper.Map(media) : null,
            Cta = cta is not null ? await _linkMapper.Map(cta) : null
        };
    }
}
