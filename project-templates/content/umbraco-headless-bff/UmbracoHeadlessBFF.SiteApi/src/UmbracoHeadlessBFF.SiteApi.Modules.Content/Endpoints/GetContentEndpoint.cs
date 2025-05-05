using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Clients.Enums;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Endpoints;

public static class GetContentEndpoint
{
    public static RouteGroupBuilder MapGetContent(this RouteGroupBuilder builder, ApiVersionSet versionSet)
    {
        builder
            .MapGet("/", GetContentHandler)
            .WithApiVersionSet(versionSet)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok, NotFound, UnauthorizedHttpResult>> GetContentHandler(
        [AsParameters] GetContentRequest request)
    {
        return await Task.FromResult(TypedResults.Ok());
    }

    private sealed record GetContentRequest
    {
        public Guid? NodeId { get; init; }
        public ContentFetchType? FetchType { get; init; }
        public IEnumerable<string>? Filter { get; init; }
        public ContentSortType? SortType { get; init; }
        public int Skip { get; init; } = 0;
        public int Take { get; init; } = 10;
        public string? Expand { get; init; }
        public string Fields { get; init; } = "properties[$all]";
    }
}
