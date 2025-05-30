namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed record Video
{
    public string? Src { get; init; }
    public string? Type { get; init; }
}
