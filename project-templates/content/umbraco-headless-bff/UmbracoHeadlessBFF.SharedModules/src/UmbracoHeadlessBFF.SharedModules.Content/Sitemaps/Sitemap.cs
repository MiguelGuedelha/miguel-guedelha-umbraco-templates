namespace UmbracoHeadlessBFF.SharedModules.Content.Sitemaps;

public sealed record Sitemap
{
    public required IReadOnlyCollection<SitemapItem> Items { get; init; }
}

public sealed record SitemapItem
{
    public required string Loc { get; init; }
    public required DateOnly LastMod { get; init; }
    public string? ChangeFrequency { get; init; }
    public decimal Priority { get; init; }
    public IReadOnlyCollection<SitemapItemAlternateLanguage>? AlternateLanguages { get; init; }
}

public sealed record SitemapItemAlternateLanguage
{
    public required string HrefLang { get; init; }
    public required string Href { get; init; }
}
