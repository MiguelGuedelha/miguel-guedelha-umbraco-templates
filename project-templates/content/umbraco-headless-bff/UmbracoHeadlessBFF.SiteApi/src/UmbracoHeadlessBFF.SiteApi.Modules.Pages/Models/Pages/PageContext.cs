namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;

internal sealed record PageContext
{
    public Seo? Seo { get; init; }
    public Breadcrumbs? Breadcrumbs { get; init; }
    public SiteSettings? SiteSettings { get; init; }
    public SiteResolutionInformation? SiteResolution { get; init; }
}
