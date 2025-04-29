namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Blocks;

public sealed class ApiBlockGridModel
{
    public ICollection<ApiBlockGridItemModel> Items { get; set; } = [];
}
