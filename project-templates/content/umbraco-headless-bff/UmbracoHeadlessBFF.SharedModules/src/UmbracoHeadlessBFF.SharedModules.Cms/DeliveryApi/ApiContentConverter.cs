using System.Text.Json;
using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;

internal sealed class ApiContentConverter : JsonConverter<IApiContent>
{
    public override IApiContent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

        var hasType = DeliveryApiConstants.ContentTypes.TypesMap.TryGetValue(discriminator, out var apiContentType);

        if (!hasType)
        {
            throw new JsonException($"No content type found for '{discriminator}'.");
        }

        return JsonSerializer.Deserialize(jsonDocument.RootElement.GetRawText(), apiContentType!, options) as IApiContent;
    }

    public override void Write(Utf8JsonWriter writer, IApiContent value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
