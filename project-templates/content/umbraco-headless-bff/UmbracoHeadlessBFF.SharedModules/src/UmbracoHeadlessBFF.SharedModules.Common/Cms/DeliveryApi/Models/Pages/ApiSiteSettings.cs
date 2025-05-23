using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed class ApiSiteSettings : IApiContent<ApiSiteSettingsProperties>
{
    public Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required ApiSiteSettingsProperties Properties { get; init; }
}

public sealed class ApiSiteSettingsProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? HeaderLogo { get; init; }
    public IReadOnlyCollection<ApiLink>? HeaderQuickLinks { get; init; }
    public ApiBlockList<ApiMainNavigationLink>? HeaderNavigation { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? FooterLogo { get; init; }
    public ApiBlockList<ApiHeadingWithLinks>? FooterLinks { get; init; }
    public ApiBlockList<ApiHeadingWithSocialLinks>? FooterSocialLinks { get; init; }
    public IReadOnlyCollection<ApiLink>? FooterFootnoteLinks { get; init; }
    public string? Copyright { get; init; }
    public IReadOnlyCollection<ApiContentReference>? NotFoundPage { get; init; }
    public IReadOnlyCollection<ApiContentReference>? SearchPage { get; init; }
    public string? CanonicalDomainOverride { get; init; }
    public string? PageTitlePrefix { get; init; }
}
