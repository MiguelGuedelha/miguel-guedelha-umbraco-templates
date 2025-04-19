using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UmbracoHeadlessBFF.SharedModules.Common.Correlation;

public sealed class HostAndPathParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        operation.Parameters.Add(new()
        {
            Name = SharedConstants.Common.Correlation.Headers.SiteHost,
            Description = "The frontend host, used to find the correct CMS instance and site node",
            In = ParameterLocation.Header,
            Required = false
        });

        operation.Parameters.Add(new()
        {
            Name = SharedConstants.Common.Correlation.Headers.SitePath,
            Description = "The frontend path, used to find the correct CMS instance and site node",
            In = ParameterLocation.Header,
            Required = false
        });

        operation.Parameters.Add(new()
        {
            Name = SharedConstants.Common.Correlation.Headers.SiteId,
            Description = "The resolved CMS/Site id, allows to skip the resolution step otherwise required if this is sent",
            In = ParameterLocation.Header,
            Required = false
        });
    }
}
