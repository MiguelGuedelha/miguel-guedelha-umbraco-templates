namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

internal sealed record SiteResolutionInformation
{
    public required string Locale { get; init; }
    public required string Domain { get; init; }
    public required string Subpath { get; init; }
    public required IReadOnlyCollection<AlternateSites> AlternateLanguages { get; init; }
}

internal sealed record AlternateSites
{
    public required string Locale { get; init; }
    public required string Domain { get; init; }
    public required string Subpath { get; init; }
}
