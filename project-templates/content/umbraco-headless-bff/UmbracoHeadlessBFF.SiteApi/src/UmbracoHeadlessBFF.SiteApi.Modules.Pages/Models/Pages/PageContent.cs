using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;

internal record PageContent
{
    public required IReadOnlyCollection<ILayout> MainContent { get; init; }
}

internal sealed record PageContent<T> : PageContent
    where T : IAdditionalProperties
{
    public required T AdditionalProperties { get; init; }
}

