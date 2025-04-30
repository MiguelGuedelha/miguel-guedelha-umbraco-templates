using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages;

public sealed class ApiSiteGrouping : IApiContent
{
    public Guid Id { get; init; }
    public string ContentType { get; } = "OneColumn";
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public required Dictionary<string, ApiContentRoute> Cultures { get; init; }
}
