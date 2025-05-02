using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.ModelsBuilder;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Components.Standard;

public class ApiRichTextSection : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => RichTextSection.ModelTypeAlias;
    public required ApiRichTextSectionProperties Properties { get; init; }
}

public class ApiRichTextSectionProperties : IApiJumpMenuConfiguration
{
    public string? JumpMenuHeading { get; init; }
    public string? JumpMenuAnchorId { get; init; }
}
