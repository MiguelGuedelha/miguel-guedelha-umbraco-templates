using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

internal sealed record RichTextSection : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public string? Text { get; init; }
    public Link? Cta { get; init; }
}
