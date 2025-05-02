using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.OEmbed;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Media;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.ModelsBuilder;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.BuildingBlocks;

public sealed class ApiEmbedVideo : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => EmbedVideo.ModelTypeAlias;
    public required ApiEmbedVideoProperties Properties { get; init; }
}

public sealed class ApiEmbedVideoProperties
{
    public ApiOEmbedItem? Video { get; init; }
    public ICollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
}
