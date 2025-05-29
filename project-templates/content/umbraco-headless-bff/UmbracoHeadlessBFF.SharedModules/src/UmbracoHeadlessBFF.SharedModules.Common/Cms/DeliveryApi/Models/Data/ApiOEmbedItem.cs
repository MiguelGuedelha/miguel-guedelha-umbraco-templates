namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed record ApiOEmbedItem
{
    public string? Url { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public string? EmbedCode { get; init; }
}
