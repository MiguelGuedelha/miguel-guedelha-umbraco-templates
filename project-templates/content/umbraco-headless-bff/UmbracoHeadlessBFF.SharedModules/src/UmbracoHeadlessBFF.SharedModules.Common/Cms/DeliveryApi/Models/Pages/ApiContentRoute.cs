namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed class ApiContentRoute
{
    public required string Path { get; init; }
    public required ApiContentStartItem StartItem { get; init; }
}
