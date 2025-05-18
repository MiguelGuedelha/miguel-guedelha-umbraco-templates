using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.OEmbed;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.MediaBlock;

internal sealed class EmbedItemMapper : IMapper<ApiOEmbedItem, EmbedItem>
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
