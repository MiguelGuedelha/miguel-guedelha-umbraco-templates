using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Layouts;

public sealed class ApiOneColumn : IApiElement
{
    public required Guid Id { get; init; }
    public required string ContentType { get; init; }
}
