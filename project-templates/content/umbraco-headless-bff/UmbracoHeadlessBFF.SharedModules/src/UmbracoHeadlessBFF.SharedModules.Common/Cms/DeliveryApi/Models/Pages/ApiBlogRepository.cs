using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Compositions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed record ApiBlogRepository : IApiContent<ApiBlogRepositoryProperties>
{
    public Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ContentTypes.ApiBlogRepository;
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required ApiBlogRepositoryProperties Properties { get; init; }
}

public sealed record ApiBlogRepositoryProperties : IApiRedirectSettingsProperties, IApiNavigationSettingsProperties
{
    public IReadOnlyCollection<ApiLink>? RedirectLink { get; init; }
    public RedirectFallbackDirection? RedirectDirection { get; init; }
    public bool ShowBreadcrumbs { get; init; }
    public bool ShowInBreadcrumbs { get; init; }
    public string? BreadcrumbNameOverride { get; init; }
    public bool ShowBreadcrumbLink { get; init; }
}
