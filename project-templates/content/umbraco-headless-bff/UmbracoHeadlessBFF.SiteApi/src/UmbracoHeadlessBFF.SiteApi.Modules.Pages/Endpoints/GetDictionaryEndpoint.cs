using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Dictionary;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Endpoints;

internal static class GetDictionaryEndpoint
{
    extension(RouteGroupBuilder builder)
    {
        public RouteGroupBuilder MapGetDictionary()
        {
            builder
                .MapGet("/dictionary", Handle)
                .MapToApiVersion(EndpointConstants.Versions.V1)
                .CacheOutput(SiteBasedOutputCachePolicy.PolicyName);

            return builder;
        }
    }

    private static async Task<Results<Ok<SiteDictionary>, NotFound>> Handle(SiteResolutionContext context, IPagesService pagesService)
    {
        var content = await pagesService.GetPage(context.Site.DictionaryId);

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
