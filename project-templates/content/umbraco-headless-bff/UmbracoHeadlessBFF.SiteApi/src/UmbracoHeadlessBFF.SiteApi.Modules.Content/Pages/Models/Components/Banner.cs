using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal sealed record Banner : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public IReadOnlyCollection<BannerItem>? Items { get; init; }
}

internal sealed record BannerItem
{
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? SubHeading { get; init; }
    public string? Description { get; init; }
    public IMediaBlock? BackgroundMedia { get; init; }
}
