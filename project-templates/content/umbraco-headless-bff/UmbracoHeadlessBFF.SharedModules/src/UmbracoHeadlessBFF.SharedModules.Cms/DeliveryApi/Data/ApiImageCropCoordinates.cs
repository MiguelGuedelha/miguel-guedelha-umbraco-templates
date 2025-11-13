namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

public sealed record ApiImageCropCoordinates
{
    public double X1 { get; init; }
    public double X2 { get; init; }
    public double Y1 { get; init; }
    public double Y2 { get; init; }
}
