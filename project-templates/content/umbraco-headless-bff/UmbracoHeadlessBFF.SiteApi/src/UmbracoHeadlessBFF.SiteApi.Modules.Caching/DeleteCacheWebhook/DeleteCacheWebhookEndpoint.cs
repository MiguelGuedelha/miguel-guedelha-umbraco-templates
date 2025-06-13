using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Endpoints;
using ZiggyCreatures.Caching.Fusion;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Caching.DeleteCacheWebhook;

internal static class DeleteCacheWebhookEndpoint
{
    public static RouteGroupBuilder DeleteCacheWebhook(this RouteGroupBuilder builder)
    {
        builder
            .MapPost("/webhook", Handle)
            .MapToApiVersion(EndpointConstants.Versions.V1);

        return builder;
    }

    private static async Task<Results<Ok, BadRequest>> Handle(HttpRequest request, IFusionCache fusionCache)
    {
        var hasHeader = request.Headers.TryGetValue("umb-webhook-event", out var umbracoEvent);

        if (!hasHeader)
        {
            return TypedResults.BadRequest();
        }

        switch (umbracoEvent.ToString())
        {
            case WebhookEventConstants.DomainDeleted:
            case WebhookEventConstants.DomainSaved:
                await fusionCache.ClearAsync();
                break;
            case WebhookEventConstants.ContentPublished:
                var publishRequest = await request.ReadFromJsonAsync<ContentPublishedRequest>();
                // TODO Handle different content types (site settings, etc)
                break;
            case WebhookEventConstants.ContentUnpublished:
            case WebhookEventConstants.ContentDeleted:
                var removalRequest = await request.ReadFromJsonAsync<ContentRemovalRequest>();
                if (removalRequest is null)
                {
                    return TypedResults.BadRequest();
                }

                await fusionCache.RemoveByTagAsync(removalRequest.Id);
                break;
            case WebhookEventConstants.ContentMoved:
            case WebhookEventConstants.ContentMovedToRecycleBin:
                var movedRequest = await request.ReadFromJsonAsync<IEnumerable<ContentMovedEntity>>();
                if (movedRequest is null)
                {
                    return TypedResults.BadRequest();
                }

                foreach (var movedEntity in movedRequest)
                {
                    await fusionCache.RemoveByTagAsync(movedEntity.Entity.Key);
                }
                break;
        }

        return TypedResults.Ok();
    }

    private sealed record ContentPublishedRequest
    {
        public required string Id { get; init; }
        public required string ContentType { get; init; }
    }

    private sealed record ContentRemovalRequest
    {
        public required string Id { get; init; }
    }

    private sealed record ContentMovedEntity
    {
        public required EntityObject Entity { get; init; }

        public sealed record EntityObject
        {
            public required string Key { get; init; }
        }
    }
}
