namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record NavigationLink
{
    public Link? Link { get; init; }
    public IReadOnlyCollection<LinkWithSublinks>? SubLinks { get; init; }
}

internal sealed record LinkWithSublinks
{
    public Link? Link { get; init; }
    public IReadOnlyCollection<Link>? Sublinks { get; init; }
}
