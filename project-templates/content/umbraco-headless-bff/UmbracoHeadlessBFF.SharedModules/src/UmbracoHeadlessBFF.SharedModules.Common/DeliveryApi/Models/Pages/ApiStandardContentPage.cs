using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages;

public class ApiStandardContentPage : ApiContent<ApiStandardContentPageProperties>
{
    public const string ContentType = "standardContentPage";
}

public sealed class ApiStandardContentPageProperties : ApiBaseSearchablePage
{
}
