using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal interface IEmbedItemMapper : IMapper<ApiOEmbedItem, EmbedItem>
{
}

internal sealed class EmbedItemMapper : IEmbedItemMapper
{
    public Task<EmbedItem?> Map(ApiOEmbedItem model)
    {
        return Task.FromResult<EmbedItem?>(new()
        {
            Url = model.Url,
            Width = model.Width,
            Height = model.Height,
            EmbedCode = model.EmbedCode,
        });
    }
}
