using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal interface IMediaLibraryVideoMapper : IMapper<ApiMediaLibraryVideo, MediaLibraryVideo>
{
}

internal sealed class MediaLibraryVideoMapper : IMediaLibraryVideoMapper
{
    private readonly IImageMapper _imageMapper;
    private readonly IVideoMapper _videoMapper;

    public MediaLibraryVideoMapper(IImageMapper imageMapper, IVideoMapper videoMapper)
    {
        _imageMapper = imageMapper;
        _videoMapper = videoMapper;
    }

    public async Task<MediaLibraryVideo?> Map(ApiMediaLibraryVideo model)
    {
        var placeholder = model.Properties.PlaceholderImage?.FirstOrDefault();
        var video = model.Properties.Video?.FirstOrDefault();

        return new()
        {
            Video = video is not null ? await _videoMapper.Map(video) : null,
            PlaceholderImage = placeholder is not null ? await _imageMapper.Map(placeholder) : null,
        };
    }
}
