using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using UmbracoHeadlessBFF.SharedModules.Common;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Cms;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Content;
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

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = false;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.AddServiceDefaults();

builder.AddCachingSharedModules();
builder.AddCorrelationSharedModules();
builder.AddCmsSharedModules();
builder.AddErrors();
builder.AddCms();

if (environment.IsLocal())
{
    configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseCorrelationSharedModules();

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.MapScalarApiReference(options =>
    {
        var descriptions = app.DescribeApiVersions();

        var documents = descriptions
            .Select(x => new ScalarDocument(
                x.GroupName.ToUpperInvariant(),
                $"Content API - {x.GroupName}",
                $"/swagger/{x.GroupName}/swagger.json")
            );

        options.AddDocuments(documents);

        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseErrors();
app.UseCms();

app.MapDefaultEndpoints();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(1.0)
    .ReportApiVersions()
    .Build();

app.MapContentEndpoints(apiVersionSet);

await app.RunAsync();
