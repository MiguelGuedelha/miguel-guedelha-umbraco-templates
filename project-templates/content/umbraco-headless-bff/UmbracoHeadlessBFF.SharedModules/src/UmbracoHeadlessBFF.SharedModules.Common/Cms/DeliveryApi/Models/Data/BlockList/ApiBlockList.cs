using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockList;

public sealed class ApiBlockList
{
    public IReadOnlyCollection<ApiBlockListItem> Items { get; init; } = [];
}

public sealed class ApiBlockList<TContent>
    where TContent : class, IApiElement
{
    public IReadOnlyCollection<ApiBlockListItem<TContent>> Items { get; init; } = [];
}
