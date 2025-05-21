using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Urls;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks.Media;

internal interface IVideoMapper : IMapper<ApiMediaWithCrops, Video>
{
}

internal sealed class VideoMapper : IVideoMapper
{
    private readonly ApplicationUrlOptions _applicationUrlOptions;

    public VideoMapper(IOptionsSnapshot<ApplicationUrlOptions> applicationUrlOptions)
    {
        _applicationUrlOptions = applicationUrlOptions.Value;
    }

    public Task<Video?> Map(ApiMediaWithCrops model)
    {
        return Task.FromResult<Video?>(new()
        {
            Src = new UriBuilder(_applicationUrlOptions.Media) { Path = model.Url}.Uri.AbsoluteUri,
            Type = $"video/{model.Extension}",
        });
    }
}
