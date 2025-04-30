namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Pages.Abstractions;

public interface IApiContent
{
    Guid Id { get; init; }
    string ContentType { get; }
    string Name { get; init; }
    DateTime CreateDate { get; init; }
    DateTime UpdateDate { get; init; }
    ApiContentRoute Route { get; init; }
    Dictionary<string, ApiContentRoute> Cultures { get; init; }
}
