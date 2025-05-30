using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

internal record PageContent
{
    public required IReadOnlyCollection<ILayout> MainContent { get; init; }
}

internal sealed record PageContent<T> : PageContent
    where T : IAdditionalProperties
{
    public required T AdditionalProperties { get; init; }
}

