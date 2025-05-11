using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;

public static class GetItemByPathEndpoint
{
    public static RouteGroupBuilder MapGetContent(this RouteGroupBuilder builder)
    {
        builder
            .MapGet("/item", GetContentHandler);
            //.MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok<IApiContent>, NotFound, UnauthorizedHttpResult>> GetContentHandler(string? path, Guid? id, [FromServices] ContentService contentService)
    {
        var content = (path, id) switch
        {
            (_, not null) => await contentService.GetContentById(id.Value),
            (not null, _) => await contentService.GetContentByPath(path),
            _ => throw new SiteApiException(StatusCodes.Status400BadRequest, "Path or id required")
        };

        return TypedResults.Ok(content);
    }
}
