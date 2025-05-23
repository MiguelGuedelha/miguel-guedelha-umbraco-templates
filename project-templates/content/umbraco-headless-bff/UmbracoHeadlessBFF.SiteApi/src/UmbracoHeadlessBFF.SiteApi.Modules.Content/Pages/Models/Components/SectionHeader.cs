namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal sealed class SectionHeader : Component
{
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? SubHeading { get; init; }
}
