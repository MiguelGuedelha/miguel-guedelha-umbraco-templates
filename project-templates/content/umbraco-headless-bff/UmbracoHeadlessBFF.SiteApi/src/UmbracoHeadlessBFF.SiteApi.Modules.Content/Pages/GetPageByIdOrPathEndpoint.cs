using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages;

public static class GetPageByIdOrPathEndpoint
{
    public static RouteGroupBuilder MapGetPage(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/page", GetPageHandler)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<IPage>, NotFound>> GetPageHandler(string id,
        ContentService contentService,
        IEnumerable<IPageMapper> mappers)
    {
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
