using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.BuildingBlocks;

internal sealed class Card
{
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? SubHeading { get; init; }
    public ResponsiveImage? Image { get; init; }
    public Link? Link { get; init; }
}
