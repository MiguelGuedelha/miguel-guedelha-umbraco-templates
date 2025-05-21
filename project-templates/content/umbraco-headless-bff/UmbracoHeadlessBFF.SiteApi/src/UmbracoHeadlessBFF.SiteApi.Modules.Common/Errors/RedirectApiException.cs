﻿using Microsoft.AspNetCore.Http;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

public sealed class RedirectApiException : Exception
{
    public string Location { get; private set; }
    public bool IsPermanent { get; private set; }

    public RedirectApiException(string location)
    {
        Location = location;
        IsPermanent = false;
    }

    public RedirectApiException(int statusCode, string location)
    {
        Location = location;
        IsPermanent = statusCode is StatusCodes.Status301MovedPermanently or StatusCodes.Status308PermanentRedirect;
    }
}
