using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;

internal interface IEmbedVideoMapper : IMapper<ApiEmbedVideo, EmbedVideo>
{
}

internal sealed class EmbedVideoMapper : IEmbedVideoMapper
{
    private readonly IImageMapper _imageMapper;
    //TODO: Embed convertion to v16

    public EmbedVideoMapper(IImageMapper imageMapper)
    {
        _imageMapper = imageMapper;
    }

    public async Task<EmbedVideo?> Map(ApiEmbedVideo model)
    {
        var placeholder = model.Properties.PlaceholderImage?.FirstOrDefault();

        return new()
        {
            PlaceholderImage = placeholder is not null ? await _imageMapper.Map(placeholder) : null
        };
    }
}
