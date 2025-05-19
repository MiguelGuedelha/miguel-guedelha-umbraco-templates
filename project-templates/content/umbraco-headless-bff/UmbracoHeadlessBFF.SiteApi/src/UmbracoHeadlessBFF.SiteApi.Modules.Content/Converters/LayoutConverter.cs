using System.Text.Json;
using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Layouts.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Converters;

internal sealed class LayoutConverter : JsonConverter<ILayout>
{
    public override ILayout? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ILayout value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
