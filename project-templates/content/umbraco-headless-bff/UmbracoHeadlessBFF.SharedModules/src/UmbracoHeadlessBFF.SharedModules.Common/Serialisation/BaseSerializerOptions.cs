using System.Text.Json;
using System.Text.Json.Serialization;

namespace UmbracoHeadlessBFF.SharedModules.Common.Serialisation;

public static class BaseSerializerOptions
{
    public static readonly JsonSerializerOptions SystemTextJsonSerializerOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() },
        ReferenceHandler = ReferenceHandler.IgnoreCycles
    };
}
