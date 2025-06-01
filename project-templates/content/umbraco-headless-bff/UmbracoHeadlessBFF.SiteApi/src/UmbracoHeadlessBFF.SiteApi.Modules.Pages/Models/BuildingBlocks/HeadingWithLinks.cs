namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record HeadingWithLinks
{
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public IReadOnlyCollection<Link>? Links { get; init; }
}
