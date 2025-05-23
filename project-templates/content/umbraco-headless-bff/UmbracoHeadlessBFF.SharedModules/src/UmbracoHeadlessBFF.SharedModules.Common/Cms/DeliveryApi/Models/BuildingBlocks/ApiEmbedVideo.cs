using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiEmbedVideo : IApiElement<ApiEmbedVideoProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiEmbedVideoProperties Properties { get; init; }
}

public sealed class ApiEmbedVideoProperties
{
    public ApiOEmbedItem? Video { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
