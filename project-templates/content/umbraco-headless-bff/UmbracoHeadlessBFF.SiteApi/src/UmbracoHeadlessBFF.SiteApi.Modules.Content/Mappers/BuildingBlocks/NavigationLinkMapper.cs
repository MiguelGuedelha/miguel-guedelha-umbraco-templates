using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal interface INavigationLinkMapper : IMapper<ApiMainNavigationLink, NavigationLink>
{
}

internal sealed class NavigationLinkMapper : INavigationLinkMapper
{
    private readonly ILinkMapper _linkMapper;
    private readonly ILinkWithSublinksMapper _linkWithSublinksMapper;

    public NavigationLinkMapper(ILinkMapper linkMapper, ILinkWithSublinksMapper linkWithSublinksMapper)
    {
        _linkMapper = linkMapper;
        _linkWithSublinksMapper = linkWithSublinksMapper;
    }

    public async Task<NavigationLink?> Map(ApiMainNavigationLink model)
    {
        var link = model.Properties.Link?.FirstOrDefault();

        var subLinksTasks = model.Properties.SubLinks?.Items
            .Select(x => _linkWithSublinksMapper.Map(x.Content)).ToArray() ?? [];
        await Task.WhenAll(subLinksTasks);

        return new()
        {
            Link = link is not null ? await _linkMapper.Map(link) : null,
            SubLinks = subLinksTasks.Select(x => x.Result).OfType<LinkWithSublinks>().ToArray()
        };
    }
}
