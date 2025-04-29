namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Media;

public sealed class ImageCropModel
{
    public string? Alias { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public required ImageCropCoordinatesModel Coordinates { get; init; }
}
