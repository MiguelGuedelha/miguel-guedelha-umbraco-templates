namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

public sealed record ApiImageCrop
{
    public string? Alias { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public required ApiImageCropCoordinates Coordinates { get; init; }
}
