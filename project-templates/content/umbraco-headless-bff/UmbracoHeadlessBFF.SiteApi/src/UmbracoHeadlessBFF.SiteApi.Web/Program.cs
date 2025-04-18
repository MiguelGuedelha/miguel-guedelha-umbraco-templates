using Asp.Versioning;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using UmbracoHeadlessBFF.SharedModules.Common;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;
using UmbracoHeadlessBFF.SiteApi.Web.Swagger;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
    options.OperationFilter<HostAndPathParameters>();
    options.OperationFilter<PreviewModeParameters>();
});

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
        context.ProblemDetails.Extensions.TryAdd("correlationId", context.HttpContext.Request.Headers[SharedConstants.Common.Correlation.Headers.CorrelationId]);
    };
});

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = false;
        options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.AddServiceDefaults();

builder.AddCaching();
builder.AddCorrelation();

if (environment.IsLocal())
{
    configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCorrelation();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseExceptionHandler();

app.MapDefaultEndpoints();

await app.RunAsync();
