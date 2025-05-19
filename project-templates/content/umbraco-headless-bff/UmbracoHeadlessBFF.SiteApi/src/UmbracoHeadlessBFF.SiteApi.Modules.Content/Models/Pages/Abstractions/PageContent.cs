using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

internal sealed class PageContent<T>
    where T : IAdditionalProperties
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required IReadOnlyCollection<ILayout> MainContent { get; init; }
    public required T AdditionalProperties { get; init; }
}
