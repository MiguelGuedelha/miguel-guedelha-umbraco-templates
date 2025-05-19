using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

internal class PageContent
{
    public required IReadOnlyCollection<ILayout> MainContent { get; init; }
}

internal sealed class PageContent<T> : PageContent
    where T : IAdditionalProperties
{
    public required T AdditionalProperties { get; init; }
}

