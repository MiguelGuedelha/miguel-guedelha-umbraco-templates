using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal interface ILinkWithSublinksMapper : IMapper<ApiLinkWithSublinks, LinkWithSublinks>
{
}

internal sealed class LinkWithSublinksMapper : ILinkWithSublinksMapper
{
    private readonly ILinkMapper _linkMapper;

    public LinkWithSublinksMapper(ILinkMapper linkMapper)
    {
        _linkMapper = linkMapper;
    }

    public async Task<LinkWithSublinks?> Map(ApiLinkWithSublinks model)
    {
        var link = model.Properties.Link?.FirstOrDefault();

        var subLinksTasks = model.Properties.SubLinks?.Select(x => _linkMapper.Map(x)).ToArray() ?? [];
        await Task.WhenAll(subLinksTasks);

        return new()
        {
            Link = link is not null ? await _linkMapper.Map(link) : null,
            Sublinks = subLinksTasks.Select(x => x.Result).OfType<Link>().ToArray()
        };
    }
}
