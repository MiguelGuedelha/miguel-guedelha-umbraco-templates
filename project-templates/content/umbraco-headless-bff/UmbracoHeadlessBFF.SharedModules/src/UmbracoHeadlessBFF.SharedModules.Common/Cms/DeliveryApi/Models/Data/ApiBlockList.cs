namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed record ApiBlockList
{
    public IReadOnlyCollection<ApiBlockListItem> Items { get; init; } = [];
}

public sealed record ApiBlockList<TContent>
    where TContent : class, IApiElement
{
    public IReadOnlyCollection<ApiBlockListItem<TContent>> Items { get; init; } = [];
}
