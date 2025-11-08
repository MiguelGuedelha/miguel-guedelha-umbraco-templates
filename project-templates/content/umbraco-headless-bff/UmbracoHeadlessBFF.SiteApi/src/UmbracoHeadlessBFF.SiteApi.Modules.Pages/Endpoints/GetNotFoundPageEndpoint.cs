using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Endpoints;

internal static class GetNotFoundPageEndpoint
{
    extension(RouteGroupBuilder builder)
    {
        public RouteGroupBuilder MapGetNotFoundPage()
        {
            builder
                .MapGet("/not-found", Handle)
                .MapToApiVersion(EndpointConstants.Versions.V1)
                .CacheOutput(SiteBasedOutputCachePolicy.PolicyName);

            return builder;
        }
    }

    private static async Task<Results<Ok<IPage>, NotFound>> Handle(IPagesService pagesService,
        IEnumerable<IPageMapper> mappers, SiteResolutionContext siteResolutionContext)
    {
        var notFoundId = siteResolutionContext.Site.NotFoundPageId;

        if (notFoundId is null)
        {
            return TypedResults.NotFound();
        }

        var data = await pagesService.GetPage(notFoundId.Value);

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
