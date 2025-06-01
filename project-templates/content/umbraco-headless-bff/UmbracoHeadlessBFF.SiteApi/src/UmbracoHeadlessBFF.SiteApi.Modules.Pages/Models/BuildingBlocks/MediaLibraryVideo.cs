namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record MediaLibraryVideo : IMediaBlock
{
    public Video? Video { get; init; }
    public Image? PlaceholderImage { get; init; }
}
