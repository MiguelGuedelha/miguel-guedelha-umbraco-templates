namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages.Abstractions;

public interface IApiContentResponseModel
{
    Guid Id { get; init; }
    string ContentType { get; init; }
    string Name { get; init; }
    DateTime CreateDate { get; init; }
    DateTime UpdateDate { get; init; }
    ApiContentRouteModel Route { get; init; }
    Dictionary<string, ApiContentRouteModel> Cultures { get; init; }
}
