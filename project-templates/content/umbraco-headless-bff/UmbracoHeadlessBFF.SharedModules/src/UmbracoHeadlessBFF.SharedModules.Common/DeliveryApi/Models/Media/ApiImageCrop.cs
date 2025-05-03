namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

public sealed class ApiImageCrop
{
    public string? Alias { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public required ApiImageCropCoordinates Coordinates { get; init; }
}
