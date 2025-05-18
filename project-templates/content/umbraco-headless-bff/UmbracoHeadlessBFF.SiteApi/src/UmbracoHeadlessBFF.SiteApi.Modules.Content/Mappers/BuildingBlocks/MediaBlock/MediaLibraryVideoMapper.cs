using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.MediaBlock;

internal sealed class MediaLibraryVideoMapper : IMapper<ApiMediaLibraryVideo, MediaLibraryVideo>
{
    private readonly IMapper<ApiMediaWithCrops, Image> _imageMapper;
    private readonly IMapper<ApiMediaWithCrops, Video> _videoMapper;

    public MediaLibraryVideoMapper(IMapper<ApiMediaWithCrops, Image> imageMapper,
        IMapper<ApiMediaWithCrops, Video> videoMapper)
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
