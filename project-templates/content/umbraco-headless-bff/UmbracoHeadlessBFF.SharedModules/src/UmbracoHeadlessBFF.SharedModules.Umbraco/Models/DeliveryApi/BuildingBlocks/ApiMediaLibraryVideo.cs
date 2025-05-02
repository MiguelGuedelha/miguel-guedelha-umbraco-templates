using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Media;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.ModelsBuilder;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.BuildingBlocks;

public sealed class ApiMediaLibraryVideo : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => MediaLibraryVideo.ModelTypeAlias;
    public required ApiMediaLibraryVideoProperties Properties { get; init; }
}

public sealed class ApiMediaLibraryVideoProperties
{
    public ICollection<ApiMediaWithCrops>? Video { get; init; }
    public ICollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
