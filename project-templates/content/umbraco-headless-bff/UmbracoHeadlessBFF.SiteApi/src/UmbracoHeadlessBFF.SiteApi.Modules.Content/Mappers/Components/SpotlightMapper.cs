using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class SpotlightMapper : IComponentMapper
{
    private readonly ILinkMapper _linkMapper;
    private readonly IMediaBlockMapper _mediaBlockMapper;
    private readonly IRichTextMapper _richTextMapper;

    public SpotlightMapper(ILinkMapper linkMapper, IMediaBlockMapper mediaBlockMapper, IRichTextMapper richTextMapper)
    {
        _linkMapper = linkMapper;
        _mediaBlockMapper = mediaBlockMapper;
        _richTextMapper = richTextMapper;
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
        var description = apiModel.Properties.Description;

        return new Spotlight
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Heading = apiModel.Properties.Heading,
            HeadingSize = apiModel.Properties.HeadingSize,
            Description = description is not null ? await _richTextMapper.Map(description) : null,
            Media = media is not null ? await _mediaBlockMapper.Map(media) : null,
            Cta = cta is not null ? await _linkMapper.Map(cta) : null
        };
    }
}
