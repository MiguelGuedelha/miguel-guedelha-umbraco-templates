namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Pages;

public sealed class ApiContentRoute
{
    public required string Path { get; init; }
    public required ApiContentStartItem StartItem { get; init; }
}
