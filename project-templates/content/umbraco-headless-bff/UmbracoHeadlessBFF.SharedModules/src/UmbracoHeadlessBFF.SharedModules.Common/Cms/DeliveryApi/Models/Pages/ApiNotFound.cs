using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed class ApiNotFound : IApiContent<ApiNotFoundProperties>
{
    public Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required ApiNotFoundProperties Properties { get; init; }
}

public sealed class ApiNotFoundProperties : IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
