namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

internal sealed record EmbedVideo : IMediaBlock
{
    public Image? PlaceholderImage { get; init; }
    public string? VideoId { get; init; }
    public string? Provider { get; init; }
}
