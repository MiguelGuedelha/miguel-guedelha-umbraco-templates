namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

internal sealed class PageContext
{
    public Seo? Seo { get; init; }
    public required SiteInformation Site { get; init; }
}
