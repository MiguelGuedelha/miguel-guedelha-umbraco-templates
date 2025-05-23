using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal class PageContent
{
    public required IReadOnlyCollection<ILayout> MainContent { get; init; }
}

internal sealed class PageContent<T> : PageContent
    where T : IAdditionalProperties
{
    public required T AdditionalProperties { get; init; }
}

