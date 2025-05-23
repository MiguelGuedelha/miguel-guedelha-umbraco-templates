﻿using Microsoft.Extensions.Logging;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Models.Pages;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Pages.Mappers.Pages;

internal sealed class FallbackPageMapper : IPageMapper
{
    private readonly ILogger<FallbackPageMapper> _logger;

    public FallbackPageMapper(ILogger<FallbackPageMapper> logger)
    {
        _logger = logger;
    }

    public bool CanMap(string type) => true;

    public Task<IPage?> Map(IApiContent model)
    {
        _logger.LogWarning("Fallback: Page {Id} of type {ContentType}", model.Id, model.ContentType);
        return Task.FromResult<IPage?>(null);
    }
}
