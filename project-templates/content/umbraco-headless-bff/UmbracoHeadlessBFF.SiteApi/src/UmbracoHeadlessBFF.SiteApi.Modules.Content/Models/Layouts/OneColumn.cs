using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;

internal sealed record OneColumn : ILayout
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public IReadOnlyCollection<IComponent>? Single { get; init; }
}
