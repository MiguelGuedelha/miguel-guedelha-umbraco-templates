namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed class MediaLibraryVideo : IMediaBlock
{
    public Video? Video { get; init; }
    public Image? PlaceholderImage { get; init; }
}
