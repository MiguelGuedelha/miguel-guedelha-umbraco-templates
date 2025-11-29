namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

internal sealed class BlogListing : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? SubHeading { get; init; }
    public Guid? AncestorId { get; init; }
    public required int PagedAmount { get; init; }
    public required string Sorting { get; init; }
}
