namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed class Image
{
    public required string Href { get; init; }
    public string? AltText { get; init; }
}
