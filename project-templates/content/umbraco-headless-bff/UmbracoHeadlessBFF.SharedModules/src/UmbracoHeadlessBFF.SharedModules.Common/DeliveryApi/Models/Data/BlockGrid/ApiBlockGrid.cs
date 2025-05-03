namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockGrid;

public sealed class ApiBlockGrid
{
    public ICollection<ApiBlockGridItem> Items { get; set; } = [];
}
