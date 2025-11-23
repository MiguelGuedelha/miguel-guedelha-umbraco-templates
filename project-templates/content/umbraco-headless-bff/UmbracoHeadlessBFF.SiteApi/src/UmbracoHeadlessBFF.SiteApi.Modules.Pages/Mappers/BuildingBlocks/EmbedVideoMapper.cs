using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;

internal interface IEmbedVideoMapper : IMapper<ApiEmbedVideo, EmbedVideo>
{
}

internal sealed class EmbedVideoMapper : IEmbedVideoMapper
{
    private readonly IImageMapper _imageMapper;

    public EmbedVideoMapper(IImageMapper imageMapper)
    {
        _imageMapper = imageMapper;
    }

    public async Task<EmbedVideo?> Map(ApiEmbedVideo? model)
    {
        if (model is null)
        {
            return null;
        }

        var placeholder = model.Properties.PlaceholderImage?.FirstOrDefault();

        return new()
        {
            PlaceholderImage = await _imageMapper.Map(placeholder),
            VideoId = model.Properties.VideoId,
            Provider = model.Properties.VideoProvider
        };
    }
}
