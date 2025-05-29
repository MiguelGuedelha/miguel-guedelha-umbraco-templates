using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Compositions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed record ApiBlogYear : IApiContent<ApiBlogYearProperties>
{
    public Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ContentTypes.ApiBlogYear;
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required ApiBlogYearProperties Properties { get; init; }
}

public sealed record ApiBlogYearProperties : IApiRedirectSettingsProperties, IApiNavigationSettingsProperties
{
    public IReadOnlyCollection<ApiLink>? RedirectLink { get; init; }
    public RedirectFallbackDirection? RedirectDirection { get; init; }
    public bool ShowBreadcrumbs { get; init; }
    public bool ShowInBreadcrumbs { get; init; }
    public string? BreadcrumbNameOverride { get; init; }
    public bool ShowBreadcrumbLink { get; init; }
}
