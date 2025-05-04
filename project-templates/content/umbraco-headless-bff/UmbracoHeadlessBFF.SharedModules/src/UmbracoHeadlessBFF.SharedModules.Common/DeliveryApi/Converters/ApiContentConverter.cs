using System.Text.Json;
using System.Text.Json.Serialization;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Blogs;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Errors;
using UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Models.Pages.Settings;

namespace UmbracoHeadlessBFF.SharedModules.Common.DeliveryApi.Converters;

public sealed class ApiContentConverter : JsonConverter<IApiContent>
{
    private static readonly Dictionary<string, Type> s_apiContentMap = new()
    {
        { ApiSiteGrouping.ContentType, typeof(ApiSiteGrouping) },
        { ApiHome.ContentType, typeof(ApiHome) },
        { ApiStandardContentPage.ContentType, typeof(ApiStandardContentPage) },
        { ApiSiteSearch.ContentType, typeof(ApiSiteSearch) },
        { ApiSiteDictionary.ContentType, typeof(ApiSiteDictionary) },
        { ApiNotFound.ContentType, typeof(ApiNotFound) },
        { ApiBlogRepository.ContentType, typeof(ApiBlogRepository) },
        { ApiBlogYear.ContentType, typeof(ApiBlogYear) },
        { ApiBlogMonth.ContentType, typeof(ApiBlogMonth) },
        { ApiBlogArticle.ContentType, typeof(ApiBlogArticle) },
        { ApiSiteSettings.ContentType, typeof(ApiSiteSettings) }
    };

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

        var type = s_apiContentMap.TryGetValue(discriminator, out var apiContentType);

        if (!type)
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
