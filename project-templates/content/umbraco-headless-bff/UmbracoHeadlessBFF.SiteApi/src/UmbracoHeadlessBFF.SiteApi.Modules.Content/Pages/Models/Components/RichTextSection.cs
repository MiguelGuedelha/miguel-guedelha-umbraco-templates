using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal sealed class RichTextSection : Component
{
    public string? Text { get; init; }
    public Link? Cta { get; init; }
}
