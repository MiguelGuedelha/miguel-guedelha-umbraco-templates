namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.BlockGrid;

public sealed class ApiBlockGrid
{
    public ICollection<ApiBlockGridItem> Items { get; set; } = [];
}
