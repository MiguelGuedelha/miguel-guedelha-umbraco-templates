using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Endpoints;

public static class GetPageByIdOrPathEndpoint
{
    public static RouteGroupBuilder MapGetPageByIdOrPath(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/page", Handler)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<IPage>, NotFound>> Handler(string id,
        IContentService contentService,
        IEnumerable<IPageMapper> mappers)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new SiteApiException(StatusCodes.Status400BadRequest, "No page id provided (path or key)");
        }

        var content = id switch
        {
            _ when Guid.TryParse(id, out var parsedId) => await contentService.GetContentById(parsedId),
            _ => await contentService.GetContentByPath(id)
        };

        if (content is null)
        {
            return TypedResults.NotFound();
        }

        var mapper = mappers.FirstOrDefault(x => x.CanMap(content.ContentType));

        if (mapper is null)
        {
            throw new SiteApiException($"No mapper for page with id = {id}");
        }

        var mapped = await mapper.Map(content);

        return mapped switch
        {
            null => TypedResults.NotFound(),
            _ => TypedResults.Ok(mapped)
        };
    }
}
