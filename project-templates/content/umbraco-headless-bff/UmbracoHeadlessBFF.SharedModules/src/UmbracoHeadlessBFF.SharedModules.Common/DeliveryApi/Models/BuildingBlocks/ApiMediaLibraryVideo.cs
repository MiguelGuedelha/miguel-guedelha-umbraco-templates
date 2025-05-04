using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Media;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.BuildingBlocks;

public sealed class ApiMediaLibraryVideo : ApiElement<ApiMediaLibraryVideoProperties>
{
    public const string ContentType = "mediaLibraryVideo";
}

public sealed class ApiMediaLibraryVideoProperties
{
    public IReadOnlyCollection<ApiMediaWithCrops>? Video { get; init; }
    public ApiMediaWithCrops? VideoItem => Video?.FirstOrDefault();
    public IReadOnlyCollection<ApiMediaWithCrops>? PlaceholderImage { get; init; }
    public ApiMediaWithCrops? PlaceholderImageItem => PlaceholderImage?.FirstOrDefault();
}
