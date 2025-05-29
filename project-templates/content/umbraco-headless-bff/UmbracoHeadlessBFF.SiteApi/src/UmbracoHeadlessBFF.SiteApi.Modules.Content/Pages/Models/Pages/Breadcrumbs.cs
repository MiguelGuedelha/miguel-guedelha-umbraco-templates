namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

internal sealed record Breadcrumbs
{
    public required IReadOnlyCollection<BreadcrumbItem> Items { get; init; }
}

internal sealed record BreadcrumbItem
{
    public required string Name { get; init; }
    public string? Href { get; init; }
}
