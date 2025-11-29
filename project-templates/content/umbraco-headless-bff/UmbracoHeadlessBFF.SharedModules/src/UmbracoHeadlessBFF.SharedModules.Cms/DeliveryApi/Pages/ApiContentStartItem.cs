namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

public sealed record ApiContentStartItem
{
    public required Guid Id { get; init; }
    public required string Path { get; init; }
}
