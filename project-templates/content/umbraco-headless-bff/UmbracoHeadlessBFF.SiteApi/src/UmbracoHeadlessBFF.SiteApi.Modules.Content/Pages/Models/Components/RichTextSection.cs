using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal sealed class RichTextSection : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public string? Text { get; init; }
    public Link? Cta { get; init; }
}
