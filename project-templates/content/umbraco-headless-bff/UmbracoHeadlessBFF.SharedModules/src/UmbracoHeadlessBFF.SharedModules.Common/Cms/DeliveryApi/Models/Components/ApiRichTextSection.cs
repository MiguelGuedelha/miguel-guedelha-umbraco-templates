using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components.Compositions;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed record ApiRichTextSection : IApiElement<ApiRichTextSectionProperties>
{
    public required Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ElementTypes.ApiRichTextSection;
    public required ApiRichTextSectionProperties Properties { get; init; }
}

public sealed record ApiRichTextSectionProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiRichTextItem? Text { get; init; }
    public IReadOnlyCollection<ApiLink>? Cta { get; init; }
}
