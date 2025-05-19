using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.Media;

internal sealed class MediaBlockMapper : IMapper<IApiElement, IMediaBlock>
{
    private readonly IMapper<ApiEmbedVideo, EmbedVideo> _embedVideoMapper;
    private readonly IMapper<ApiResponsiveImage, ResponsiveImage> _responsiveImageMapper;
    private readonly IMapper<ApiMediaLibraryVideo, MediaLibraryVideo> _mediaLibraryVideoMapper;

    public MediaBlockMapper(IMapper<ApiEmbedVideo, EmbedVideo> embedVideoMapper,
        IMapper<ApiResponsiveImage, ResponsiveImage> responsiveImageMapper,
        IMapper<ApiMediaLibraryVideo, MediaLibraryVideo> mediaLibraryVideoMapper)
    {
        _embedVideoMapper = embedVideoMapper;
        _responsiveImageMapper = responsiveImageMapper;
        _mediaLibraryVideoMapper = mediaLibraryVideoMapper;
    }

    public async Task<IMediaBlock?> Map(IApiElement model)
    {
        return model switch
        {
            ApiEmbedVideo embedVideo => await _embedVideoMapper.Map(embedVideo),
            ApiResponsiveImage responsiveImage => await _responsiveImageMapper.Map(responsiveImage),
            ApiMediaLibraryVideo mediaLibraryVideo => await _mediaLibraryVideoMapper.Map(mediaLibraryVideo),
            _ => throw new InvalidOperationException("Unmappable type passed in")
        };
    }
}
