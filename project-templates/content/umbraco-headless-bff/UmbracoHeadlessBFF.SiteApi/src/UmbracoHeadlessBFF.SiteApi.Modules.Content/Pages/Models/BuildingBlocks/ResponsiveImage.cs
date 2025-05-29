namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

internal sealed record ResponsiveImage : IMediaBlock
{
    public Image? MainImage { get; init; }
    public Image? MobileImage { get; init; }
    public string? AltText { get; init; }
}
