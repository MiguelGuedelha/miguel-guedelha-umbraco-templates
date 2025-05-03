using System.Text.Json.Serialization;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Data.Links;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LinkType
{
    Content,
    Media,
    External
}
