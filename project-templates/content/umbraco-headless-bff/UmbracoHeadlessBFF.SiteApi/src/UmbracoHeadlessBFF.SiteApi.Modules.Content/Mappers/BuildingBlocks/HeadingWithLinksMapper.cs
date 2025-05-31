using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.BuildingBlocks;

internal interface IHeadingWithLinksMapper : IMapper<ApiHeadingWithLinks, HeadingWithLinks>
{
}

internal sealed class HeadingWithLinksMapper : IHeadingWithLinksMapper
{
    private readonly ILinkMapper _linkMapper;

    public HeadingWithLinksMapper(ILinkMapper linkMapper)
    {
        _linkMapper = linkMapper;
    }

    public async Task<HeadingWithLinks?> Map(ApiHeadingWithLinks model)
    {
        var linkTasks = model.Properties.Links?.Select(x => _linkMapper.Map(x)).ToArray() ?? [];
        await Task.WhenAll(linkTasks);

        return new()
        {
            Heading = model.Properties.Heading,
            HeadingSize = model.Properties.HeadingSize,
            Links = linkTasks.Select(x => x.Result).OfType<Link>().ToArray()
        };
    }
}
