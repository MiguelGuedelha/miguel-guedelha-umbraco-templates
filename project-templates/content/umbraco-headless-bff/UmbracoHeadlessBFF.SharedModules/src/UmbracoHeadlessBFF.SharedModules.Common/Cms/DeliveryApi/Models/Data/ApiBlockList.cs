namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed class ApiBlockList
{
    public IReadOnlyCollection<ApiBlockListItem> Items { get; init; } = [];
}

public sealed class ApiBlockList<TContent>
    where TContent : class, IApiElement
{
    public IReadOnlyCollection<ApiBlockListItem<TContent>> Items { get; init; } = [];
}
