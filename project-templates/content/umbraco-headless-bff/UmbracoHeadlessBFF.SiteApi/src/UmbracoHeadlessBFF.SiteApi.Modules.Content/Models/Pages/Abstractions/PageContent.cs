using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

internal sealed class PageContent<T>
    where T : IAdditionalProperties
{
    public required string ContentType { get; init; }
    public required string Id { get; init; }
    public required IReadOnlyCollection<IComponent> Components { get; init; }
    public required T AdditionalProperties { get; init; }
}
