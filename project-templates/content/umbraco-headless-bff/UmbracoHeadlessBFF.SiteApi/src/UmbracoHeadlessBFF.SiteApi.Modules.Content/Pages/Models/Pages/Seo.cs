namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed record Seo
{
    public required string CanonicalUrl { get; init; }
    public string? MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaImage { get; init; }
    public string? OgType { get; init; }
    public string? OgDescription { get; init; }

    public string? OgImage { get; init; }

    public string? RobotsIndexOptions { get; init; }
    public DateTime? RobotsUnavailableAfter { get; init; }
}
