using System.Text.Json;
using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Converters;

internal sealed class PageConverter : JsonConverter<IPage>
{
    public override IPage? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IPage value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
