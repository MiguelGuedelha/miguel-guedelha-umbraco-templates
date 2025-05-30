namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed record Image
{
    public required string Src { get; init; }
    public string? AltText { get; init; }
}
