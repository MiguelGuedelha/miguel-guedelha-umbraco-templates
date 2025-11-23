using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;

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

    public async Task<MediaLibraryVideo?> Map(ApiMediaLibraryVideo? model)
    {
        if (model is null)
        {
            return null;
        }

        var placeholder = model.Properties.PlaceholderImage?.FirstOrDefault();
        var video = model.Properties.Video?.FirstOrDefault();

        return new()
        {
            Video = await _videoMapper.Map(video),
            PlaceholderImage = await _imageMapper.Map(placeholder),
        };
    }
}
