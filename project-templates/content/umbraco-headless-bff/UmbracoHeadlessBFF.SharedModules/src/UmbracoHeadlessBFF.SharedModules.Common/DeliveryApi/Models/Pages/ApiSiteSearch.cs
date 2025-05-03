using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages;

public sealed class ApiSiteSearch : ApiContent<ApiSiteSearchProperties>
{
    public const string ContentType = "siteSearch";
}

public sealed class ApiSiteSearchProperties : ApiSeoSettingsProperties
{
}
