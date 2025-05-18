using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.OEmbed;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.MediaBlock;

internal sealed class EmbedVideoMapper : IMapper<ApiEmbedVideo, EmbedVideo>
{
    private readonly IMapper<ApiMediaWithCrops, Image> _imageMapper;
    private readonly IMapper<ApiOEmbedItem, EmbedItem> _embedItemMapper;

    public EmbedVideoMapper(IMapper<ApiMediaWithCrops, Image> imageMapper,
        IMapper<ApiOEmbedItem, EmbedItem> embedItemMapper)
    {
        _imageMapper = imageMapper;
        _embedItemMapper = embedItemMapper;
    }

    public async Task<EmbedVideo?> Map(ApiEmbedVideo model)
    {
        var video = model.Properties.Video;
        var placeholder = model.Properties.PlaceholderImage?.FirstOrDefault();

        return new()
        {
            Video = video is not null ? await _embedItemMapper.Map(video) : null,
            PlaceholderImage = placeholder is not null ? await _imageMapper.Map(placeholder) : null
        };
    }
}
