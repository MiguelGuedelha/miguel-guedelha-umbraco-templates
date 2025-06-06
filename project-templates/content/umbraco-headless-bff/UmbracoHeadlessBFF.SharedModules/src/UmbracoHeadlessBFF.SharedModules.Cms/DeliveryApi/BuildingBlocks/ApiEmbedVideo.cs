using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Data;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.BuildingBlocks;

public sealed record ApiEmbedVideo : IApiElement<ApiEmbedVideoProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiEmbedVideo;
    public required ApiEmbedVideoProperties Properties { get; init; }
}

public sealed record ApiEmbedVideoProperties
{
    public ApiOEmbedItem? Video { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
