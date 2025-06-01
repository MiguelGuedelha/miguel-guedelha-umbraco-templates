using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

internal sealed record Spotlight : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? Description { get; init; }
    public Link? Cta { get; init; }
    public IMediaBlock? Media { get; init; }
}
