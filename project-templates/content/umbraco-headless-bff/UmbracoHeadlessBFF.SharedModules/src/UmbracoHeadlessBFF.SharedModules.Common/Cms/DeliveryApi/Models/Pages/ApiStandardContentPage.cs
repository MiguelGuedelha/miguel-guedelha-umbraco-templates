using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockGrid;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Shared;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed class ApiStandardContentPage : ApiContent<ApiStandardContentPageProperties>
{
}

public sealed class ApiStandardContentPageProperties : ApiBaseSearchablePageProperties, IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
