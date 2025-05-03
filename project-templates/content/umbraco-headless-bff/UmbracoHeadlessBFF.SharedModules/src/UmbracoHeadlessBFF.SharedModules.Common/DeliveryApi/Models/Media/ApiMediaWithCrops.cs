using System.Text.Json.Serialization;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

public sealed class ApiMediaWithCrops
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string MediaType { get; init; }
    public required string Url { get; init; }
    public string? Extension { get; init; }
    public int? Width { get; init; }
    public int? Height { get; init; }
    public int? Bytes { get; init; }
    [JsonExtensionData]
    public required IDictionary<string, object> Properties { get; init; }
    public ApiImageFocalPoint? FocalPoint { get; init; }
    public ICollection<ApiImageCrop> Crops { get; init; } = [];
}
