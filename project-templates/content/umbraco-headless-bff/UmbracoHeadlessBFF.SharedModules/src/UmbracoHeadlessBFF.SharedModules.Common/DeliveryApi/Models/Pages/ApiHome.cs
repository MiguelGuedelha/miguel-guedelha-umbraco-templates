using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages;

public sealed class ApiHome : ApiContent<ApiHomeProperties>
{
    public const string ContentType = "home";
}

public sealed class ApiHomeProperties : ApiBaseNonSearchablePage
{
}
