using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.Media;

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
