using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Errors;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Settings;

public sealed class ApiSiteSettings : ApiContent<ApiSiteSettingsProperties>
{
    public const string ContentType = "siteSettings";
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
    public string? Copyright  { get; init; }
    public IReadOnlyCollection<ApiNotFound>? NotFoundPage { get; init; }
    public IReadOnlyCollection<ApiSiteSearch>? SearchPage { get; init; }
    public string? CanonicalDomainOverride { get; init; }
    public string? PageTitlePrefix { get; init; }
}
