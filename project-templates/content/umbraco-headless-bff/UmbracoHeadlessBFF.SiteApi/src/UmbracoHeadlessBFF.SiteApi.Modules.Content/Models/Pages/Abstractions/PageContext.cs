namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

internal sealed class PageContext
{
    public required Seo Seo { get; init; }
    public required SiteInformation Site { get; init; }
}
