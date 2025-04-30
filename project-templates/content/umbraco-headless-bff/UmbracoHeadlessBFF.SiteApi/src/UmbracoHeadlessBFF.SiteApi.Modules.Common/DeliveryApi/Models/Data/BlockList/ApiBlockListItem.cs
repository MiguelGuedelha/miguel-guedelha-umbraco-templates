using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.BlockList;

public sealed class ApiBlockListItem : IApiBlockListItem
{
    public required IApiElement Content { get; init; }
    public IApiElement? Settings { get; init; }
}
