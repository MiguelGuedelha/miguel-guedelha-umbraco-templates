namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

internal sealed class Image
{
    public required string Src { get; init; }
    public string? AltText { get; init; }
}
