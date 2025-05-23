namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;

public sealed class PagedApiContent
{
    public int Total { get; init; }
    public required IReadOnlyCollection<IApiContent> Items { get; init; }
}
