using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Layouts;

internal sealed class OneColumn : Layout
{
    public required IReadOnlyCollection<IComponent?> Single { get; init; }
}
