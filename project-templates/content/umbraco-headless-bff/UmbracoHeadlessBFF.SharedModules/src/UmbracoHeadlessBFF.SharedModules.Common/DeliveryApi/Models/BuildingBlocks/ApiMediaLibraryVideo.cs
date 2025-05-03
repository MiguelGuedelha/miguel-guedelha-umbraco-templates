using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiMediaLibraryVideo : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => "mediaLibraryVideo";
    public required ApiMediaLibraryVideoProperties Properties { get; init; }
}

public sealed class ApiMediaLibraryVideoProperties
{
    public ICollection<ApiMediaWithCrops>? Video { get; init; }
    public ICollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
