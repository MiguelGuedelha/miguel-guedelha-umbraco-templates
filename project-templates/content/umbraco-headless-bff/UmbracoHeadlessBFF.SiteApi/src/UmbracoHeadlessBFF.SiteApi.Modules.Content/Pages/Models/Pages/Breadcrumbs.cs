namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed class Breadcrumbs
{
    public required IReadOnlyCollection<BreadcrumbItem> Items { get; init; }
}

internal sealed class BreadcrumbItem
{
    public required string Name { get; init; }
    public string? Href { get; init; }
}
