using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.MediaBlock;

internal sealed class VideoMapper : IMapper<ApiMediaWithCrops, Video>
{
    public Task<Video?> Map(ApiMediaWithCrops model)
    {
        return Task.FromResult<Video?>(new()
        {
            Src = model.Url,
            Type = $"video/{model.Extension}",
        });
    }
}
