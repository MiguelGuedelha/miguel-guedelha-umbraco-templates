using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.Policies;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms.SiteResolution;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Mappers.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Pages;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Endpoints;

internal static class GetPageByIdOrPathEndpoint
{
    extension(RouteGroupBuilder builder)
    {
        public RouteGroupBuilder MapGetPageByIdOrPath()
        {
            builder
                .MapGet("/page", Handle)
                .MapToApiVersion(EndpointConstants.Versions.V1)
                .CacheOutput(SiteAndIdBasedOutputCachePolicy.PolicyName);

            return builder;
        }
    }

    private static async Task<Results<Ok<IPage>, NotFound>> Handle(
        string id,
        IPagesService pagesService,
        IEnumerable<IPageMapper> mappers,
        SiteResolutionContext siteResolutionContext)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new SiteApiException(StatusCodes.Status400BadRequest, "No page id provided (path or key)");
        }

        var content = id switch
        {
            _ when Guid.TryParse(id, out var parsedId) => await pagesService.GetPage(parsedId),
            _ => await pagesService.GetPage(id)
        };

        if (content is null)
        {
            return TypedResults.NotFound();
        }

        siteResolutionContext.PageId = content.Id;

        var mapper = mappers.FirstOrDefault(x => x.CanMap(content.ContentType)) ?? throw new SiteApiException($"No mapper for page with id = {id}");
        var mapped = await mapper.Map(content);

        return mapped switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(mapped)
        };
    }
}
