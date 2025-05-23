namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;

internal sealed class Image
{
    public required string Src { get; init; }
    public string? AltText { get; init; }
}
