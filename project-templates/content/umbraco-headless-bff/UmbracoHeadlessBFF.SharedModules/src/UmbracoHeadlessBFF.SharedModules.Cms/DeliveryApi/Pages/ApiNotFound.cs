using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages.Compositions;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

public sealed record ApiNotFound : IApiContent<ApiNotFoundProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ContentTypes.ApiNotFound;
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required ApiNotFoundProperties Properties { get; init; }
}

public sealed record ApiNotFoundProperties : IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
