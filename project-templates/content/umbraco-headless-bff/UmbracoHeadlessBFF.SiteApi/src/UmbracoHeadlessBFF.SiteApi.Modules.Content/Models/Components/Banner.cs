using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

internal sealed class Banner : Component
{
    public IReadOnlyCollection<BannerItem>? Items { get; init; }
}

internal sealed class BannerItem
{
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? SubHeading { get; init; }
    public string? Description { get; init; }
    public IMediaBlock? BackgroundMedia { get; init; }
}
