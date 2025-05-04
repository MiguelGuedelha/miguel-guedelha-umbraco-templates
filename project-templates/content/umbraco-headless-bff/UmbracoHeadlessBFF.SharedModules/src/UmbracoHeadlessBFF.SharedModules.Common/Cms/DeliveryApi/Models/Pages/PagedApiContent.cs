using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public class PagedApiContent
{
    public int Total { get; init; }
    public required IReadOnlyCollection<IApiContent> Items { get; init; }
}
