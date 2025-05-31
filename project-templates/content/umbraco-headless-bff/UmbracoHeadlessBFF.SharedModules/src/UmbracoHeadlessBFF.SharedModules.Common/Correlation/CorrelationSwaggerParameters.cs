using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UmbracoHeadlessBFF.SharedModules.Common.Correlation;

public sealed class CorrelationSwaggerParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        operation.Parameters.Add(new()
        {
            Name = CorrelationConstants.Headers.SiteHost,
            Description = "The frontend host, used to find the correct CMS instance and site node",
            In = ParameterLocation.Header,
            Required = false
        });

        operation.Parameters.Add(new()
        {
            Name = CorrelationConstants.Headers.SitePath,
            Description = "The frontend path, used to find the correct CMS instance and site node",
            In = ParameterLocation.Header,
            Required = false
        });

        operation.Parameters.Add(new()
        {
            Name = CorrelationConstants.Headers.SiteId,
            Description = "The resolved CMS/Site id, allows to skip the resolution step otherwise required if this is sent",
            In = ParameterLocation.Header,
            Required = false
        });

        operation.Parameters.Add(new()
        {
            Name = CorrelationConstants.Headers.PreviewMode,
            Description = "Whether or not the API should return preview content",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new()
            {
                Type = "boolean"
            }
        });

        operation.Parameters.Add(new()
        {
            Name = CorrelationConstants.Headers.PreviewToken,
            Description = "The preview token to be used when preview mode is true",
            In = ParameterLocation.Header,
            Required = false
        });
    }
}
