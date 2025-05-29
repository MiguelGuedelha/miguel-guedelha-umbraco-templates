using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.BuildingBlocks;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

internal sealed record Gallery : IComponent
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public string? Heading { get; init; }
    public required string HeadingSize { get; init; }
    public string? SubHeading { get; init; }
    public IReadOnlyCollection<Card>? Items { get; init; }
    public required int CardsPerRow { get; init; }
}
