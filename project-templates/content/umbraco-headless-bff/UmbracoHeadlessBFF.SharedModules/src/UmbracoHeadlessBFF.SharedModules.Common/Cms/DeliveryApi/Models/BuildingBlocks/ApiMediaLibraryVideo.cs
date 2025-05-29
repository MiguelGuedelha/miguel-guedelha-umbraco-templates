using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed record ApiMediaLibraryVideo : IApiElement<ApiMediaLibraryVideoProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiMediaLibraryVideo;
    public required ApiMediaLibraryVideoProperties Properties { get; init; }
}

public sealed record ApiMediaLibraryVideoProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? Video { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
