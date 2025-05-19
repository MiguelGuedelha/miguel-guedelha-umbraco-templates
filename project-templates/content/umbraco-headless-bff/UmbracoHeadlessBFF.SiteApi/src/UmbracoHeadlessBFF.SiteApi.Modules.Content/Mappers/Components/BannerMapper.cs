using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Banner;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class BannerMapper : IComponentMapper
{
    private readonly IMapper<IApiElement, IMediaBlock> _mediaBlockMapper;

    public BannerMapper(IMapper<IApiElement, IMediaBlock> mediaBlockMapper)
    {
        _mediaBlockMapper = mediaBlockMapper;
    }

    public bool CanMap(string type) => type == DeliveryApiConstants.ElementTypes.ApiBanner;

    public async Task<IComponent?> Map(IApiElement model, IApiElement? settings)
    {
        if (model is not ApiBanner apiModel)
        {
            return null;
        }

        return new Banner
        {
            Id = apiModel.Id,
            ContentType = apiModel.ContentType,
            Items = await MapItems(apiModel.Properties.Items?.Items.Select(x => x.Content) ?? [])
        };
    }

    private async Task<IReadOnlyCollection<BannerItem>> MapItems(IEnumerable<ApiBannerItem> bannerItems)
    {
        var mapTasks = bannerItems.Select(async x =>
        {
            var media = x.Properties.BackgroundMedia?.Items.FirstOrDefault()?.Content;

            return new BannerItem
            {
                Heading = x.Properties.Heading,
                HeadingSize = x.Properties.HeadingSize,
                SubHeading = x.Properties.SubHeading,
                Description = x.Properties.Description?.Markup,
                BackgroundMedia = media is not null ? await _mediaBlockMapper.Map(media) : null
            };
        }).ToList();

        await Task.WhenAll(mapTasks);

        return mapTasks.Select(x => x.Result).ToList();
    }
}
