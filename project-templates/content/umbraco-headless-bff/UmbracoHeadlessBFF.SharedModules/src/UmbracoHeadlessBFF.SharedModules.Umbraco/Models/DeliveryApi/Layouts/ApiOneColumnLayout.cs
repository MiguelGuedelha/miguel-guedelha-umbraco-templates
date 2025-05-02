using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Data.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Umbraco.Models.ModelsBuilder;

namespace UmbracoHeadlessBFF.SharedModules.Umbraco.Models.DeliveryApi.Layouts;

public class ApiOneColumnLayout : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => OneColumn.ModelTypeAlias;
}
