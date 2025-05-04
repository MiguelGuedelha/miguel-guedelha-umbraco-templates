using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.RichText;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Components.Standard;

public sealed class ApiRichTextSection : ApiElement<ApiRichTextSectionProperties>
{
    public const string ContentType = "richTextSection";
}

public sealed class ApiRichTextSectionProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiRichTextItem? Text { get; init; }
    public IReadOnlyCollection<ApiLink>? Cta { get; init; }
    public ApiLink? CtaItem => Cta?.FirstOrDefault();
}
