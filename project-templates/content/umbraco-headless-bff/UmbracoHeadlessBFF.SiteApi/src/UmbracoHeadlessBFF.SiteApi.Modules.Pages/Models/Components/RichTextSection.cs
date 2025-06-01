using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

internal sealed record RichTextSection : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public string? Text { get; init; }
    public Link? Cta { get; init; }
}
