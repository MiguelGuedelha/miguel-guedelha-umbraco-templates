using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Pages.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;

public static class GetItemByPathEndpoint
{
    public static RouteGroupBuilder MapGetContent(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/item", GetContentHandler)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<IPage>, NotFound, UnauthorizedHttpResult>> GetContentHandler(string id,
        [FromServices] ContentService contentService,
        [FromServices] IEnumerable<IPageMapper> mappers)
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

        var mapped = await mappers.First(x => x.CanMap(content.ContentType)).Map(content);

        if (mapped is null)
        {
            throw new SiteApiException($"Error mapping content for id = {id}");
        }

        return TypedResults.Ok(mapped);
    }
}
