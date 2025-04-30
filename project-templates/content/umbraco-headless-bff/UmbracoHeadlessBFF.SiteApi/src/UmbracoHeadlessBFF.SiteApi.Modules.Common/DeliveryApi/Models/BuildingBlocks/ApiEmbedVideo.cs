using UmbracoHeadlessBFF.SharedModules.Umbraco.Models;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.OEmbed;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.BuildingBlocks;

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
