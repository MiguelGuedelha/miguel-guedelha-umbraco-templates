using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Components;

internal sealed class BannerMapper : IComponentMapper
{
    private readonly IMediaBlockMapper _mediaBlockMapper;
    private readonly IRichTextMapper _richTextMapper;

    public BannerMapper(IMediaBlockMapper mediaBlockMapper, IRichTextMapper richTextMapper)
    {
        _mediaBlockMapper = mediaBlockMapper;
        _richTextMapper = richTextMapper;
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
            var description = x.Properties.Description;

            return new BannerItem
            {
                Heading = x.Properties.Heading,
                HeadingSize = x.Properties.HeadingSize,
                SubHeading = x.Properties.SubHeading,
                Description = description is not null ? await _richTextMapper.Map(description) : null,
                BackgroundMedia = media is not null ? await _mediaBlockMapper.Map(media) : null
            };
        }).ToArray();

        await Task.WhenAll(mapTasks);

        return mapTasks.Select(x => x.Result).ToArray();
    }
}
