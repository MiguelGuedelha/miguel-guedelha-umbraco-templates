using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UmbracoHeadlessBFF.SiteApi.Web.Swagger;

internal sealed class PreviewModeParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        operation.Parameters.Add(new()
        {
            Name = "previewMode",
            In = ParameterLocation.Query,
            Description = "Only required for endpoints that have different outputs between on preview mode being enabled or not",
            Schema = new()
            {
                Type = "boolean"
            },
            Required = false,
        });

        operation.Parameters.Add(new()
        {
            Name = "previewToken",
            In = ParameterLocation.Query,
            Description = "Only required for endpoints that have different outputs between on preview mode being enabled or not",
            Required = false
        });
    }
}
