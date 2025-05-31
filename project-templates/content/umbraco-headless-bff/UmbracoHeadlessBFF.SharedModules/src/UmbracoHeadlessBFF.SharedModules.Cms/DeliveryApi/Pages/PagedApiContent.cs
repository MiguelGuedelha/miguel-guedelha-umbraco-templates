namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

public sealed record PagedApiContent
{
    public int Total { get; init; }
    public required IReadOnlyCollection<IApiContent> Items { get; init; }
}
