using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.BlockList;

public sealed class ApiBlockListItem : IApiBlockListItem
{
    public required IApiElement Content { get; init; }
    public IApiElement? Settings { get; init; }
}
