namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

internal sealed class HeadingWithSocialLinks
{
    public required string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public IReadOnlyCollection<SocialLink>? SocialLinks { get; init; }
}
