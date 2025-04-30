using UmbracoHeadlessBFF.SharedModules.Umbraco.Models;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.DeliveryApi.Models.Layouts;

public class ApiOneColumnLayout : IApiElement
{
    public required string Id { get; init; }
    public string ContentType => OneColumn.ModelTypeAlias;
}
