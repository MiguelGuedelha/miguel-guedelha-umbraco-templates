using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed class ApiSiteSettings : ApiContent<ApiSiteSettingsProperties>
{
}

public sealed class ApiSiteSettingsProperties : RedirectSettingsProperties
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
