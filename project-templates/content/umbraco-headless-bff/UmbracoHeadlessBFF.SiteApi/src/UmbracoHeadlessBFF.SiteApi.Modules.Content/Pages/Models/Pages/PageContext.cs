namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed class PageContext
{
    public required Seo? Seo { get; init; }
    public required Breadcrumbs? Breadcrumbs { get; init; }
    public required SiteSettings? SiteSettings { get; init; }
    public required SiteResolutionInformation SiteResolution { get; init; }
}
