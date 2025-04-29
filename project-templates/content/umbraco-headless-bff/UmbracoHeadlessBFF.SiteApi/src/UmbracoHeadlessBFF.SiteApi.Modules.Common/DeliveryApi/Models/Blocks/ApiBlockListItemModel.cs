using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks;

public sealed class ApiBlockListItemModel : IApiBlockItemModel
{
    public required IApiElementModel Content { get; init; }
    public IApiElementModel? Settings { get; init; }
}
