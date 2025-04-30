using UmbracoHeadlessBFF.SharedModules.Umbraco.Models;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.BuildingBlocks;

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
