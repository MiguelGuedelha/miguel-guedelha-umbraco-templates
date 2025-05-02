namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Media;

public sealed class ApiImageCrop
{
    public string? Alias { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public required ApiImageCropCoordinates Coordinates { get; init; }
}
