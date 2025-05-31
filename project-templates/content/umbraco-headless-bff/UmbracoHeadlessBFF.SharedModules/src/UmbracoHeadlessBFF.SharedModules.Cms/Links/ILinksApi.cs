﻿using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Cms.Links;

public interface ILinksApi
{
    [Get("/{id}")]
    Task<ApiResponse<Link>> GetLink(Guid id, string culture, bool preview);

    [Get("/redirects/{path}")]
    Task<ApiResponse<RedirectLink>> GetRedirect(string path, Guid siteId, string culture);
}
