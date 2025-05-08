using Microsoft.AspNetCore.Http;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;

public class SiteApiException : Exception
{
    public int StatusCode { get; private set; }

    public SiteApiException()
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }

    public SiteApiException(int statusCode)
    {
        StatusCode = statusCode;
    }

    public SiteApiException(string? message) : base(message)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }

    public SiteApiException(int statusCode, string? message) : base(message)
    {
        StatusCode = statusCode;
    }

    public SiteApiException(string? message, Exception? inner) : base(message, inner)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }

    public SiteApiException(int statusCode, string? message, Exception? inner) : base(message, inner)
    {
        StatusCode = statusCode;
    }
}
