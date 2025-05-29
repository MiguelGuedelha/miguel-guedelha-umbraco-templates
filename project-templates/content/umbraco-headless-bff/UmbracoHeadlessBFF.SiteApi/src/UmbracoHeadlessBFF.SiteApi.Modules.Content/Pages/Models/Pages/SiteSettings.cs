using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed record SiteSettings
{
    public required Header Header { get; init; }
    public required Footer Footer { get; init; }
    public string? SearchPage { get; init; }
}

internal sealed record Header
{
    public string? Logo { get; init; }
    public IReadOnlyCollection<Link>? QuickLinks { get; init; }
    public IReadOnlyCollection<NavigationLink>? Navigation { get; init; }
}

internal sealed record Footer
{
    public string? Logo { get; init; }
    public IReadOnlyCollection<HeadingWithLinks>? Links { get; init; }
    public HeadingWithSocialLinks? SocialLinks { get; init; }
    public IReadOnlyCollection<Link>? FootnoteLinks { get; init; }
    public string? Copyright { get; init; }
}
