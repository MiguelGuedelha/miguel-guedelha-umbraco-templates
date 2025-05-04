using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockGrid;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Errors;

public sealed class ApiNotFound : ApiContent<ApiNotFoundProperties>
{
    public const string ContentType = "notFound";
}

public sealed class ApiNotFoundProperties : ApiBaseNonSearchablePageProperties, IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
