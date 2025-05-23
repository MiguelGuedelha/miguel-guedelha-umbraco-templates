using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiMediaLibraryVideo : IApiElement<ApiMediaLibraryVideoProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiMediaLibraryVideoProperties Properties { get; init; }
}

public sealed class ApiMediaLibraryVideoProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? Video { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
