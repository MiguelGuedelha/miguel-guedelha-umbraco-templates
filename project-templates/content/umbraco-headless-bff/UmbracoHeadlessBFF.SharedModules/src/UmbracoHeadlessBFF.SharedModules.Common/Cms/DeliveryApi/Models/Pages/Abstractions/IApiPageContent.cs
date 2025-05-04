using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockGrid;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

public interface IApiPageContent
{
    public ApiBlockGrid MainContent { get; init; }
}

public abstract class ApiPageContent : IApiPageContent
{
    public required ApiBlockGrid MainContent { get; init; }
}
