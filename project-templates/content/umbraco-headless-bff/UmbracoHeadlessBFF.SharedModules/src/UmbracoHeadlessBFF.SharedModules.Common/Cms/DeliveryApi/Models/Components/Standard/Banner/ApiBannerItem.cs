using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.RichText;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Standard.Banner;

public sealed class ApiBannerItem : ApiElement, IApiHeadingWithSubHeading, IApiRteDescription
{
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h2";
    public string? SubHeading { get; init; }
    public ApiRichTextItem? Description { get; init; }
    public ApiBlockList? BackgroundMedia { get; init; }
    public IApiElement? BackgroundMediaItem => BackgroundMedia?.Items.FirstOrDefault()?.Content;
}
