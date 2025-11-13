namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages.Compositions;

public interface IApiNavigationSettingsProperties
{
    public bool ShowBreadcrumbs { get; init; }
    public bool ShowInBreadcrumbs { get; init; }
    public string? BreadcrumbNameOverride { get; init; }
    public bool ShowBreadcrumbLink { get; init; }
}
