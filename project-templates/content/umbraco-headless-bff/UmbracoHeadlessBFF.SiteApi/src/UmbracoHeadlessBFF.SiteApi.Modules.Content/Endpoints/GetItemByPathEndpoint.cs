using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

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

    private static async Task<Results<Ok<List<IComponent?>>, NotFound, UnauthorizedHttpResult>> GetContentHandler(string id,
        [FromServices] ContentService contentService,
        [FromServices] IEnumerable<IComponentMapper> mappers)
    {
        var content = id switch
        {
            _ when Guid.TryParse(id, out var parsedId) => await contentService.GetContentById(parsedId),
            _ => await contentService.GetContentByPath(id)
        };

        /* TODO: Temporary Debug - Remove */
        var home = content as ApiHome;

        var components = home?.Properties.MainContent.Items
            .SelectMany(x => x.Areas)
            .SelectMany(x => x.Items)
            .Select(x =>
            {
                var mapper = mappers.First(mapper => mapper.CanMap(x.Content.ContentType));
                return mapper.Map(x.Content, x.Settings);
            }).ToList() ?? [];

        await Task.WhenAll(components);

        var mapped = components
            .Select(x => x.Result)
            .Where(x => x is not null)
            .ToList();
        /*-------------------------------*/

        return TypedResults.Ok(mapped);
    }
}
