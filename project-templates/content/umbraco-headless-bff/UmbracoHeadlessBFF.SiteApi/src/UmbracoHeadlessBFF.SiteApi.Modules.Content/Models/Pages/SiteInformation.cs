namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

internal sealed class SiteInformation
{
    public required string Locale { get; init; }
    public required string Domain { get; init; }
    public required string Subpath { get; init; }
    public required IReadOnlyCollection<AlternateSites> AlternateLanguages { get; init; }
}

internal sealed class AlternateSites
{
    public required string Locale { get; init; }
    public required string Domain { get; init; }
    public required string Subpath { get; init; }
}
