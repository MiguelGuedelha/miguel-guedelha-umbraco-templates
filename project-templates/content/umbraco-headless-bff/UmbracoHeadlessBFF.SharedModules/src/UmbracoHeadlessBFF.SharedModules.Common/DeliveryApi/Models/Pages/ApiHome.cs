using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockGrid;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages;

public sealed class ApiHome : ApiContent<ApiHomeProperties>
{
    public const string ContentType = "home";
}

public sealed class ApiHomeProperties : ApiBaseNonSearchablePageProperties, IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
