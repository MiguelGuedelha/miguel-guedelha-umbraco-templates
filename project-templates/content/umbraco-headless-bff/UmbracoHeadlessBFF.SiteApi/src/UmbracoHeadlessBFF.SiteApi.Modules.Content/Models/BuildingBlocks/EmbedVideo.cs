namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed class EmbedVideo : IMediaBlock
{
    public EmbedItem? Video { get; init; }
    public Image? PlaceholderImage { get; init; }
}
