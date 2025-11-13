namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

public sealed record ApiContentRoute
{
    public required string Path { get; init; }
    public required ApiContentStartItem StartItem { get; init; }
}
