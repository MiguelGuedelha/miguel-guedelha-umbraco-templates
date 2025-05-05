using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.GetContent;

public static class GetContentEndpoint
{
    public static RouteGroupBuilder MapGetContent(this RouteGroupBuilder builder, ApiVersionSet versionSet)
    {
        builder.MapGet("/", GetContentHandler)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(EndpointConstants.Versions.V1);
        return builder;
    }

    private static async Task<Results<Ok, NotFound, UnauthorizedHttpResult>> GetContentHandler([AsParameters] GetContentRequest request)
    {
        return TypedResults.Ok();
    }

    private record struct GetContentRequest
    {
        public Guid? NodeId { get; init; }

    }
}
