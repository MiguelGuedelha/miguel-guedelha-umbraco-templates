using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

internal sealed class RichTextSection : Component
{
    public string? Text { get; init; }
    public Link? Cta { get; init; }
}
