using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal interface ICardMapper : IMapper<IEnumerable<ApiCard>, IReadOnlyCollection<Card>>,
    IMapper<ApiCard, Card>
{
}

internal sealed class CardMapper : ICardMapper
{
    private readonly IResponsiveImageMapper _responsiveImageMapper;
    private readonly ILinkMapper _linkMapper;

    public CardMapper(IResponsiveImageMapper responsiveImageMapper, ILinkMapper linkMapper)
    {
        _responsiveImageMapper = responsiveImageMapper;
        _linkMapper = linkMapper;
    }

    public async Task<IReadOnlyCollection<Card>?> Map(IEnumerable<ApiCard> model)
    {
        var mapTasks = model.Select(Map).ToArray();

        await Task.WhenAll(mapTasks);

        return mapTasks.Select(x => x.Result).OfType<Card>().ToArray();
    }

    public async Task<Card?> Map(ApiCard model)
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
