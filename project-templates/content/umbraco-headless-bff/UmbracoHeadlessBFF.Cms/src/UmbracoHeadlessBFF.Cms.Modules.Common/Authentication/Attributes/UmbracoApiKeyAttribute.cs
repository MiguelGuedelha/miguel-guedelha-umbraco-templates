using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Authentication.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class UmbracoApiKeyAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeaderName = "x-api-key";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var deliveryApiConfig = context.HttpContext.RequestServices.GetService<IOptions<DeliveryApiSettings>>();
        var expectedApiKey = deliveryApiConfig?.Value.ApiKey;

        if (string.IsNullOrWhiteSpace(expectedApiKey) ||
            !context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var providedApiKey) ||
            !string.Equals(providedApiKey, expectedApiKey, StringComparison.Ordinal))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
