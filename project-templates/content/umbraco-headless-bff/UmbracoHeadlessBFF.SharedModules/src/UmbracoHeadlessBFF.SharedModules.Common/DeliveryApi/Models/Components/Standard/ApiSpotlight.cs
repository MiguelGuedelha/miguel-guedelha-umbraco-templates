using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.BlockList;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.RichText;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard;

public sealed class ApiSpotlight : ApiElement<ApiSpotlightProperties>
{
    public const string ContentType = "spotlight";
}

public sealed class ApiSpotlightProperties : IApiJumpMenuConfiguration, IApiHeading, IApiRteDescription
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public string? Heading { get; init; }
    public string HeadingSize { get; init; } = "h2";
    public ApiRichTextItem? Description { get; init; }
    public IReadOnlyCollection<ApiLink>? Cta { get; init; }
    public ApiLink? CtaItem => Cta?.FirstOrDefault();
    public ApiBlockList? Media { get; init; }
    public IApiElement? MediaItem => Media?.Items.FirstOrDefault()?.Content;
}
