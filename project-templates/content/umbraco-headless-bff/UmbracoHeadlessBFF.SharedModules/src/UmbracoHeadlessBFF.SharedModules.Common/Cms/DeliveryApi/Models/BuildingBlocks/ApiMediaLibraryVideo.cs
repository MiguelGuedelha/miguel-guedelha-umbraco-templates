using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiMediaLibraryVideo : IApiElement<ApiMediaLibraryVideoProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiMediaLibraryVideo;
    public required ApiMediaLibraryVideoProperties Properties { get; init; }
}

public sealed class ApiMediaLibraryVideoProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? Video { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
