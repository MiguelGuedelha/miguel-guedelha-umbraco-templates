using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms;

public class CmsSwaggerParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        operation.Parameters.Add(new()
        {
            Name = DeliveryApiConstants.ApiKeyHeaderName,
            Description = "The api key to be used when making requests",
            In = ParameterLocation.Header,
            Required = true,
        });
    }
}
