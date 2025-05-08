using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;

public static class GetItemByPathEndpoint
{
    public static RouteGroupBuilder MapGetContent(this RouteGroupBuilder builder, ApiVersionSet versionSet)
    {
        builder
            .MapGet("/item/{path}", GetContentHandler)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok, NotFound, UnauthorizedHttpResult>> GetContentHandler(string path)
    {
        return await Task.FromResult(TypedResults.Ok());
    }
}
