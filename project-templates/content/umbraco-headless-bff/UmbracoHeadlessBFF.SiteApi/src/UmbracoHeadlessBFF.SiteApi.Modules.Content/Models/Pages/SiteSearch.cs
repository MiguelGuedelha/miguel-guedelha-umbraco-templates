namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages;

internal sealed record SiteSearch : IPage
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required PageContext Context { get; init; }
}
