namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record EmbedVideo : IMediaBlock
{
    public Image? PlaceholderImage { get; init; }
}
