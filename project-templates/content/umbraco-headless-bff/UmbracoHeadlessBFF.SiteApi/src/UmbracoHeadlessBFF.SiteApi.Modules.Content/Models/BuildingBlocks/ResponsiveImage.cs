using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed class ResponsiveImage : IMediaBlock
{
    public Image? MainImage { get; init; }
    public Image? MobileImage { get; init; }
    public string? AltText { get; init; }
}
