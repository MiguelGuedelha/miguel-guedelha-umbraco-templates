namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

internal sealed class NavigationLink
{
    public required Link? Link { get; init; }
    public required IReadOnlyCollection<LinkWithSublinks>? SubLinks { get; init; }
}

internal sealed class LinkWithSublinks
{
    public required Link? Link { get; init; }
    public required IReadOnlyCollection<Link>? Sublinks { get; init; }
}
