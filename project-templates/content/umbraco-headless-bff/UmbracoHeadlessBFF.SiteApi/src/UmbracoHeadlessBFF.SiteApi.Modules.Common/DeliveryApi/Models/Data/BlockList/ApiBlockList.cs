using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.BlockList;

public sealed class ApiBlockList
{
    public ICollection<IApiBlock> Items { get; init; } = [];
}

public sealed class ApiBlockList<TContent>
    where TContent : class, IApiElement
{
    public ICollection<IApiBlock<TContent>> Items { get; init; } = [];
}
