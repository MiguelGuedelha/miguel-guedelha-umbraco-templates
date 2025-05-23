using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed class ApiRichTextSection : ApiElement<ApiRichTextSectionProperties>
{
}

public sealed class ApiRichTextSectionProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiRichTextItem? Text { get; init; }
    public IReadOnlyCollection<ApiLink>? Cta { get; init; }
}
