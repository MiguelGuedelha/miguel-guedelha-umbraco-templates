using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal sealed class CardMapper : IMapper<IEnumerable<ApiCard>, IReadOnlyCollection<Card>>
{
    private readonly IMapper<ApiResponsiveImage, ResponsiveImage> _responsiveImageMapper;
    private readonly IMapper<ApiLink, Link> _linkMapper;

    public CardMapper(IMapper<ApiResponsiveImage, ResponsiveImage> responsiveImageMapper, IMapper<ApiLink, Link> linkMapper)
    {
        _responsiveImageMapper = responsiveImageMapper;
        _linkMapper = linkMapper;
    }

    public async Task<IReadOnlyCollection<Card>?> Map(IEnumerable<ApiCard> model)
    {
        var mapTasks = model.Select(Map).ToList();

        await Task.WhenAll(mapTasks);

        return mapTasks.Select(x => x.Result).OfType<Card>().ToArray();
    }

    private async Task<Card?> Map(ApiCard model)
    {
        var image = model.Properties.Image?.Items.FirstOrDefault()?.Content;
        var link = model.Properties.Link?.FirstOrDefault();

        return new()
        {
            Heading = model.Properties.Heading,
            HeadingSize = model.Properties.HeadingSize,
            SubHeading = model.Properties.SubHeading,
            Image = image is not null ? await _responsiveImageMapper.Map(image) : null,
            Link = link is not null ? await _linkMapper.Map(link) : null
        };
    }
}
