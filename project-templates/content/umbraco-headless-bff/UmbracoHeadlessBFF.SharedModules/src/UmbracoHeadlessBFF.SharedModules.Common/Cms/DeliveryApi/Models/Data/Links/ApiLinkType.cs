using System.Text.Json.Serialization;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Links;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApiLinkType
{
    Content,
    Media,
    External
}
