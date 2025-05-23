using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umbraco.Cms.Core.Configuration.Models;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class ApiKeyAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var deliveryApiConfig = context.HttpContext.RequestServices.GetService<IOptions<DeliveryApiSettings>>();
        var expectedApiKey = deliveryApiConfig?.Value.ApiKey;

        if (string.IsNullOrWhiteSpace(expectedApiKey) ||
            !context.HttpContext.Request.Headers.TryGetValue(DeliveryApiConstants.ApiKeyHeaderName, out var providedApiKey) ||
            !string.Equals(providedApiKey, expectedApiKey, StringComparison.Ordinal))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
