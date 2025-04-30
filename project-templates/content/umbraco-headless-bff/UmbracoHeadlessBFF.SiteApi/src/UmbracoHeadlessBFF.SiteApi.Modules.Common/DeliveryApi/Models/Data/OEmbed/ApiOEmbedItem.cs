namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.OEmbed;

public sealed class ApiOEmbedItem
{
    public string? Url { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public string? EmbedCode { get; init; }
}
