using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages.Compositions;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

public sealed record ApiBlogMonth : IApiContent<ApiBlogMonthProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ContentTypes.ApiBlogMonth;
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required ApiBlogMonthProperties Properties { get; init; }
}

public sealed record ApiBlogMonthProperties : IApiRedirectSettingsProperties, IApiNavigationSettingsProperties
{
    public IReadOnlyCollection<ApiLink>? RedirectLink { get; init; }
    public RedirectFallbackDirection? RedirectDirection { get; init; }
    public bool ShowBreadcrumbs { get; init; }
    public bool ShowInBreadcrumbs { get; init; }
    public string? BreadcrumbNameOverride { get; init; }
    public bool ShowBreadcrumbLink { get; init; }
}
