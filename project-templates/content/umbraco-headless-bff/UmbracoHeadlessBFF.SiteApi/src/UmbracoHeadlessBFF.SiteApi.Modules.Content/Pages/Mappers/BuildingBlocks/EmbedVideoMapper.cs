using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;

internal interface IEmbedVideoMapper : IMapper<ApiEmbedVideo, EmbedVideo>
{
}

internal sealed class EmbedVideoMapper : IEmbedVideoMapper
{
    private readonly IImageMapper _imageMapper;
    private readonly IEmbedItemMapper _embedItemMapper;

    public EmbedVideoMapper(IImageMapper imageMapper, IEmbedItemMapper embedItemMapper)
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
