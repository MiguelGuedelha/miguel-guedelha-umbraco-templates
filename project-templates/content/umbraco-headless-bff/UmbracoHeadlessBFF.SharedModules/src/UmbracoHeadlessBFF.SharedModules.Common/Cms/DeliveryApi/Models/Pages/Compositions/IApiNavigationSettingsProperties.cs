namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Compositions;

public interface IApiNavigationSettingsProperties
{
    public bool ShowBreadcrumbs { get; init; }
    public bool ShowInBreadcrumbs { get; init; }
    public string? BreadcrumbNameOverride { get; init; }
    public bool ShowBreadcrumbLink { get; init; }
}
