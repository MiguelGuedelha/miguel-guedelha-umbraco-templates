namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

public sealed record ApiBlockList
{
    public IReadOnlyCollection<ApiBlockListItem> Items { get; init; } = [];
}

public sealed record ApiBlockList<TContent>
    where TContent : class, IApiElement
{
    public IReadOnlyCollection<ApiBlockListItem<TContent>> Items { get; init; } = [];
}
