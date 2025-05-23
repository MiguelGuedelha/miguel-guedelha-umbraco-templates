using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;

internal sealed class OneColumn : Layout
{
    public required IReadOnlyCollection<IComponent?> Single { get; init; }
}
