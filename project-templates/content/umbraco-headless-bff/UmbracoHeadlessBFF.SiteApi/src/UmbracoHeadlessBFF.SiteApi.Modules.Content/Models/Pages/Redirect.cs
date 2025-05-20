using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

internal sealed class Redirect : Page
{
    public required string RedirectUrl { get; init; }
}
