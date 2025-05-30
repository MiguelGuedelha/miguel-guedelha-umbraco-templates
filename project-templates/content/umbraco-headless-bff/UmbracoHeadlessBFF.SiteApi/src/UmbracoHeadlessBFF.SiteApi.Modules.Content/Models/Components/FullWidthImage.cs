using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

internal sealed record FullWidthImage : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public ResponsiveImage? Image { get; init; }
}
