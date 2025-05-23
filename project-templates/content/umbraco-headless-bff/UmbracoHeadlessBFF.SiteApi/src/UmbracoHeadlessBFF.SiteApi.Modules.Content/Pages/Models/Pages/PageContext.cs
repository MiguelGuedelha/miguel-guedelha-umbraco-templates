namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed class PageContext
{
    public Seo? Seo { get; init; }
    public required SiteInformation Site { get; init; }
}
