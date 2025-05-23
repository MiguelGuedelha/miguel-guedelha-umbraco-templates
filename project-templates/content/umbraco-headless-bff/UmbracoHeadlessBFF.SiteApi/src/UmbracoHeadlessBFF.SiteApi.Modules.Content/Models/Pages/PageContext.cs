namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Shared;

internal sealed class PageContext
{
    public Seo? Seo { get; init; }
    public required SiteInformation Site { get; init; }
}
