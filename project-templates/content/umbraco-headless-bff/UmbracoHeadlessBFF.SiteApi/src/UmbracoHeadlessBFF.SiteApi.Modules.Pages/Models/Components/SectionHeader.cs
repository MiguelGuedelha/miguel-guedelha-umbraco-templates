namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Components;

internal sealed record SectionHeader : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? SubHeading { get; init; }
}
