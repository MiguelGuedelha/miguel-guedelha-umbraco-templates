using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockGrid;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages;

public sealed class ApiStandardContentPage : ApiContent<ApiStandardContentPageProperties>
{
    public const string ContentType = "standardContentPage";
}

public sealed class ApiStandardContentPageProperties : ApiBaseSearchablePageProperties, IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
