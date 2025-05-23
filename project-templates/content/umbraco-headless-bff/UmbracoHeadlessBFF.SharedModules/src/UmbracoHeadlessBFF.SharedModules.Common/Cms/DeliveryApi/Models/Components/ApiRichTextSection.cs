using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Components;

public sealed class ApiRichTextSection : IApiElement<ApiRichTextSectionProperties>
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
    public required ApiRichTextSectionProperties Properties { get; init; }
}

public sealed class ApiRichTextSectionProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
    public ApiRichTextItem? Text { get; init; }
    public IReadOnlyCollection<ApiLink>? Cta { get; init; }
}
