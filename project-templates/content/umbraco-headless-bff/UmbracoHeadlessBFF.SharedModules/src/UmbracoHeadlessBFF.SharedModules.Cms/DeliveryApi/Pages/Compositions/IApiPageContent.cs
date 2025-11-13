using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages.Compositions;

public interface IApiPageContent
{
    public ApiBlockGrid MainContent { get; init; }
}
