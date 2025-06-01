namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record EmbedItem
{
    public string? Url { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public string? EmbedCode { get; init; }
}
