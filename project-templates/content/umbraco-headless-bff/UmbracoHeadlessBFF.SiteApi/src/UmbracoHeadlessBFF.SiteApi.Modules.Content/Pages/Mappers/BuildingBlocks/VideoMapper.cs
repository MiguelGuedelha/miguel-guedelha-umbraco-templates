using Microsoft.Extensions.Options;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Urls;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.BuildingBlocks;

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
            Src = new UriBuilder(_applicationUrlOptions.Media) { Path = model.Url }.Uri.AbsoluteUri,
            Type = $"video/{model.Extension}",
        });
    }
}
