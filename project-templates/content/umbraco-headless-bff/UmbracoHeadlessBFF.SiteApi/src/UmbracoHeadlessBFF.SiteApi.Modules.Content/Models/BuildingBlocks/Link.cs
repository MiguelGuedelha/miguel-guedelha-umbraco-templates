namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed class Link
{
    public required string Href { get; init; }
    public string? Target { get; init; }
    public string? Title { get; init; }
    public bool IsFile { get; init; }
}
