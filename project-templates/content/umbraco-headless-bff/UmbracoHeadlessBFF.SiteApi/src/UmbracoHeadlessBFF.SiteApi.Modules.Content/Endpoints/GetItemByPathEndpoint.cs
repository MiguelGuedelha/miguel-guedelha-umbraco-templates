using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;

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

    private static async Task<Results<Ok<IApiContent>, NotFound, UnauthorizedHttpResult>> GetContentHandler(string id, [FromServices] ContentService contentService)
    {
        var content = id switch
        {
            _ when Guid.TryParse(id, out var parsedId) => await contentService.GetContentById(parsedId),
            _ => await contentService.GetContentByPath(id)
        };

        return TypedResults.Ok(content);
    }
}
