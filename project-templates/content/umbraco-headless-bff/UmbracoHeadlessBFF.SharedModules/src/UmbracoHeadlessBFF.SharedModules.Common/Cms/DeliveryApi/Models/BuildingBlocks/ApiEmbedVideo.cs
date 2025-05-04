using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.OEmbed;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiEmbedVideo : ApiElement<ApiEmbedVideoProperties>
{
    public const string ContentType = "embedVideo";
}

public sealed class ApiEmbedVideoProperties
{
    public ApiOEmbedItem? Video { get; init; }
    public IReadOnlyCollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
    public ApiMediaWithCrops? PlaceholderImageItem => PlaceholderImage?.FirstOrDefault();
}
