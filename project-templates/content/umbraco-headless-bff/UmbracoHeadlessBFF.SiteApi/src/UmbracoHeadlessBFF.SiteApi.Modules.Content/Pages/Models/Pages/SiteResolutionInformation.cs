namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed class SiteResolutionInformation
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
