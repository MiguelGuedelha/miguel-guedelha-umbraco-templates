using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.OEmbed;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiEmbedVideo : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => "embedVideo";
    public required ApiEmbedVideoProperties Properties { get; init; }
}

public sealed class ApiEmbedVideoProperties
{
    public ApiOEmbedItem? Video { get; init; }
    public ICollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
