using System.Text.Json;
using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Components;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Converters;

internal sealed class ComponentConverter : JsonConverter<IComponent>
{
    public override IComponent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IComponent value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
