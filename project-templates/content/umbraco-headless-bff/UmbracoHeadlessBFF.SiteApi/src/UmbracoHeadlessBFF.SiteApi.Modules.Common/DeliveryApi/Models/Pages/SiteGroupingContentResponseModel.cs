using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages;

public sealed class SiteGroupingContentResponseModel : IApiContentResponseModel
{
    public Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRouteModel Route { get; init; }
    public required Dictionary<string, ApiContentRouteModel> Cultures { get; init; }
}
