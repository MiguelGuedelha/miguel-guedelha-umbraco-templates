namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record Link
{
    public required string Href { get; init; }
    public string? Target { get; init; }
    public string? Title { get; init; }
    public bool IsFile { get; init; }
}
