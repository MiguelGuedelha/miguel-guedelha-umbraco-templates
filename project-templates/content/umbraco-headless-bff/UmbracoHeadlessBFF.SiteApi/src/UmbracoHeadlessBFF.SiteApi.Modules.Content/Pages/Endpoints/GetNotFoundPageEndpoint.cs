using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Endpoints;

internal static class GetNotFoundPageEndpoint
{
    public static RouteGroupBuilder MapGetNotFoundPage(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/not-found", Handler)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<IPage>, NotFound>> Handler(IContentService contentService,
        IEnumerable<IPageMapper> mappers, SiteResolutionContext siteResolutionContext)
    {
        var notFoundId = siteResolutionContext.Site.NotFoundPageId;

        var data = await contentService.GetContentById(notFoundId);

        if (data is not ApiNotFound notFoundPage)
        {
            return TypedResults.NotFound();
        }

        var mapper = mappers.FirstOrDefault(x => x.CanMap(notFoundPage.ContentType));

        if (mapper is null)
        {
            throw new SiteApiException($"No mapper for {notFoundPage.ContentType} page type");
        }

        var mapped = await mapper.Map(notFoundPage);

        return mapped switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(mapped)
        };
    }
}
