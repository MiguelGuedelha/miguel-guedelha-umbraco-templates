using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;

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
