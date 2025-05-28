using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed class SiteSettings
{
    public required Header Header { get; init; }
    public required Footer Footer { get; init; }
    public required string? NotFoundPage { get; init; }
    public required string? SearchPage { get; init; }
}

internal sealed class Header
{
    public required string? Logo { get; init; }
    public required IReadOnlyCollection<Link>? QuickLinks { get; init; }
    public required IReadOnlyCollection<NavigationLink>? Navigation { get; init; }
}

internal sealed class Footer
{
    public required string? Logo { get; init; }
    public required IReadOnlyCollection<HeadingWithLinks>? Links { get; init; }
    public required HeadingWithSocialLinks? SocialLinks { get; init; }
    public required IReadOnlyCollection<Link>? FootnoteLinks { get; init; }
    public required string? Copyright { get; init; }
}
