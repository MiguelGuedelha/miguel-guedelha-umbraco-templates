using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Layouts;

internal sealed class OneColumn : ILayout
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required IReadOnlyCollection<IComponent?> Single { get; init; }
}
