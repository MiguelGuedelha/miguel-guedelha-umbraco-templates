using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.BuildingBlocks;

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

    public async Task<LinkWithSublinks?> Map(ApiLinkWithSublinks? model)
    {
        if (model is null)
        {
            return null;
        }

        var link = model.Properties.Link?.FirstOrDefault();

        var subLinksTasks = model.Properties.SubLinks?.Select(x => _linkMapper.Map(x)).ToArray() ?? [];
        await Task.WhenAll(subLinksTasks);

        return new()
        {
            Link = await _linkMapper.Map(link),
            Sublinks = subLinksTasks.Select(x => x.Result).OfType<Link>().ToArray()
        };
    }
}
