using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.BlockList;

public sealed class ApiBlockList
{
    public ICollection<IApiBlock> Items { get; init; } = [];
}

public sealed class ApiBlockList<TContent>
    where TContent : class, IApiElement
{
    public ICollection<IApiBlock<TContent>> Items { get; init; } = [];
}
