using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;

internal sealed class OneColumn : Layout
{
    public required IReadOnlyCollection<IComponent?> Single { get; init; }
}
