using System.Text.Json;
using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Converters;

public sealed class ApiElementConverter : JsonConverter<IApiElement>
{
    public override IApiElement? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);
        var hasContentType = jsonDocument.RootElement.TryGetProperty("contentType", out var contentType);

        if (!hasContentType)
        {
            throw new JsonException("No content type found.");
        }

        var discriminator = contentType.GetString();

        if (string.IsNullOrWhiteSpace(discriminator))
        {
            throw new JsonException("No discriminator");
        }

        var hasType = DeliveryApiConstants.ElementTypes.TypesMap.TryGetValue(discriminator, out var apiContentType);

        if (!hasType)
        {
            throw new JsonException($"No content type found for '{discriminator}'.");
        }

        return JsonSerializer.Deserialize(jsonDocument.RootElement.GetRawText(), apiContentType!, options) as IApiElement;
    }

    public override void Write(Utf8JsonWriter writer, IApiElement value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
