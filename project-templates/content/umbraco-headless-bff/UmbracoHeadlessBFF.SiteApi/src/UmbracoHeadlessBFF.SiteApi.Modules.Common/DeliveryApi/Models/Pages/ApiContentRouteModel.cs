namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages;

public sealed class ApiContentRouteModel
{
    public required string Path { get; init; }
    public required ApiContentStartItemModel StartItem { get; init; }
}
