namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

internal sealed class MediaLibraryVideo : IMediaBlock
{
    public Video? Video { get; init; }
    public Image? PlaceholderImage { get; init; }
}
