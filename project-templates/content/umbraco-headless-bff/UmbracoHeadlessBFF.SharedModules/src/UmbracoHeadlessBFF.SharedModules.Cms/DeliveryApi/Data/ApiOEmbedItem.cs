namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

public sealed record ApiOEmbedItem
{
    public string? Url { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public string? EmbedCode { get; init; }
}
