using System.Text.Json;
using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Layouts;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Converters;

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
