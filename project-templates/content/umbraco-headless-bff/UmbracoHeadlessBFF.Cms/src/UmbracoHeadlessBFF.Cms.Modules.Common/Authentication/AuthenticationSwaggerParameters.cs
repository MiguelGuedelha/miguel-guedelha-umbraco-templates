using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Umbraco.Cms.Core.Configuration.Models;
using UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Authentication;

public sealed class AuthenticationSwaggerParameters : IOperationFilter
{
    private readonly IOptionsMonitor<DeliveryApiSettings> _deliveryApiSettings;

    public AuthenticationSwaggerParameters(IOptionsMonitor<DeliveryApiSettings> deliveryApiSettings)
    {
        _deliveryApiSettings = deliveryApiSettings;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        var apiKeyParameter = operation.Parameters.FirstOrDefault(x => x.Name == DeliveryApiConstants.ApiKeyHeaderName);

        if (apiKeyParameter is not null
            && _deliveryApiSettings.CurrentValue is { PublicAccess: false, ApiKey: not null })
        {
            apiKeyParameter.Required = true;
            return;
        }

        operation.Parameters.Add(new()
        {
            Name = DeliveryApiConstants.ApiKeyHeaderName,
            Description = "The api key to be used when making requests",
            In = ParameterLocation.Header,
            Required = true,
        });
    }
}
