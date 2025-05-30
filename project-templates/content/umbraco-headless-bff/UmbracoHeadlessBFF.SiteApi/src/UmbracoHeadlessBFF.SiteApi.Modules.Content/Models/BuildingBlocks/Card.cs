namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed record Card
{
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? SubHeading { get; init; }
    public ResponsiveImage? Image { get; init; }
    public Link? Link { get; init; }
}
