using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Layouts;

public class ApiOneColumnLayout : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => "oneColumn";
}
