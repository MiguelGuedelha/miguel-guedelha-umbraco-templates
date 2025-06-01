namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record HeadingWithSocialLinks
{
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public IReadOnlyCollection<SocialLink>? SocialLinks { get; init; }
}
