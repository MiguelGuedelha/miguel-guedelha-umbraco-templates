using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal interface IMediaBlockMapper : IMapper<IApiElement, IMediaBlock>
{
}

internal sealed class MediaBlockMapper : IMediaBlockMapper
{
    private readonly IEmbedVideoMapper _embedVideoMapper;
    private readonly IResponsiveImageMapper _responsiveImageMapper;
    private readonly IMediaLibraryVideoMapper _mediaLibraryVideoMapper;

    public MediaBlockMapper(IEmbedVideoMapper embedVideoMapper,
        IResponsiveImageMapper responsiveImageMapper, IMediaLibraryVideoMapper mediaLibraryVideoMapper)
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
