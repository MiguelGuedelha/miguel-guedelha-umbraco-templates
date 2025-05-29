using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal sealed record FullWidthImage : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public ResponsiveImage? Image { get; init; }
}
