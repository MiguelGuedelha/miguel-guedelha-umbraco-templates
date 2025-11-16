namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;

internal sealed record Seo
{
    public required string CanonicalUrl { get; init; }
    public string? MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaImage { get; init; }
    public string? OgType { get; init; }
    public string? OgTitle { get; set; }
    public string? OgDescription { get; init; }

    public string? OgImage { get; init; }

    public string? RobotsIndexOptions { get; init; }
    public DateTimeOffset? RobotsUnavailableAfter { get; init; }
}
