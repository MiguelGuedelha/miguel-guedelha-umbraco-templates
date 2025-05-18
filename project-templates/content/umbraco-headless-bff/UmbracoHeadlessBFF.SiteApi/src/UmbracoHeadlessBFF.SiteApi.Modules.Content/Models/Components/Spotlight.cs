using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

internal sealed class Spotlight : Component
{
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? Description { get; init; }
    public Link? Cta { get; init; }
    public IMediaBlock? Media { get; init; }
}
