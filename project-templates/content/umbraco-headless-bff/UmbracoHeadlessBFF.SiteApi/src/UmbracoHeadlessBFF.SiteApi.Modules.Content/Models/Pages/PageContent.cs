using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Shared;

internal class PageContent
{
    public required IReadOnlyCollection<ILayout> MainContent { get; init; }
}

internal sealed class PageContent<T> : PageContent
    where T : IAdditionalProperties
{
    public required T AdditionalProperties { get; init; }
}

