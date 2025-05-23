using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

internal sealed class FullWidthImage : Component
{
    public ResponsiveImage? Image { get; init; }
}
