using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks;

public sealed class ApiBlockListModel
{
    public ICollection<IApiBlockModel> Items { get; set; } = [];
}
