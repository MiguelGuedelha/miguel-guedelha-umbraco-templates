namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

internal sealed class HeadingWithLinks
{
    public required string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public required IReadOnlyCollection<Link>? Links { get; init; }
}
