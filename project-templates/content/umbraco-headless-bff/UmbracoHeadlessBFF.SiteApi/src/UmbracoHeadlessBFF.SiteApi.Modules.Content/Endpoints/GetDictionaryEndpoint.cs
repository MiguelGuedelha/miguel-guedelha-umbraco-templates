using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Dictionary;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;

internal static class GetDictionaryEndpoint
{
    public static RouteGroupBuilder MapGetDictionary(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/dictionary", Handler)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<SiteDictionary>, NotFound>> Handler(SiteResolutionContext context, IContentService contentService)
    {
        var content = await contentService.GetContentById(context.Site.DictionaryId);

        if (content is not ApiSiteDictionary dictionary)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(MapSiteDictionary(dictionary));
    }

    private static SiteDictionary MapSiteDictionary(ApiSiteDictionary apiDictionary)
    {
        var props = apiDictionary.Properties;

        return new()
        {
            General = new()
            {
                Search = new()
                {
                    PlaceholderText = MapValue(props.GeneralSearchPlaceholderText, nameof(props.GeneralSearchPlaceholderText))
                },
                Buttons = new()
                {
                    Next = MapValue(props.GeneralButtonsNextText, nameof(props.GeneralButtonsNextText)),
                    Back = MapValue(props.GeneralButtonsBackText, nameof(props.GeneralButtonsBackText))
                }
            }
        };
    }

    private static string MapValue(string? value, string defaultValue)
    {
        return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    }
}
