using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using UmbracoHeadlessBFF.SharedModules.Cms;
using UmbracoHeadlessBFF.SharedModules.Common.Caching;
using UmbracoHeadlessBFF.SharedModules.Common.Correlation;
using UmbracoHeadlessBFF.SharedModules.Common.Environment;
using UmbracoHeadlessBFF.SharedModules.Common.Versioning;
using UmbracoHeadlessBFF.SiteApi.Modules.CacheInvalidation;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Cms;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Configuration;
using UmbracoHeadlessBFF.SiteApi.Modules.Common.Errors;
using UmbracoHeadlessBFF.SiteApi.Modules.Pages;
using UmbracoHeadlessBFF.SiteApi.Web.Swagger;
using CachingConstants = UmbracoHeadlessBFF.SiteApi.Modules.Common.Caching.CachingConstants;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
    options.OperationFilter<CorrelationSwaggerParameters>();
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

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.SerializerOptions.Converters.AddDeliveryApiConverters();
    options.SerializerOptions.Converters.AddPagesConverters();
});

builder.AddServiceDefaults();

builder.AddCachingSharedModule(CachingConstants.SiteApiCacheName,
    configureJsonSerializerOptions: options =>
    {
        options.Converters.Add(new JsonStringEnumConverter());
        options.Converters.AddDeliveryApiConverters();
        options.Converters.AddPagesConverters();
    });

builder.AddCorrelationCommonSharedModule();
builder.AddCmsCommonSharedModule();
builder.AddErrorsCommonModule();
builder.AddConfigurationCommonModule();
builder.AddCachingCommonModule();
builder.AddCmsCommonModule();
builder.AddPagesModule();
builder.AddCacheInvalidationModule();

if (environment.IsLocal())
{
    configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseCorrelationSharedModules();

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

app.UseExceptionHandler();

app.UseCmsCommonModuleMiddleware();
app.UseOutputCache();

app.MapDefaultEndpoints();

var apiVersionSet = app
    .MapGroup("/api")
    .NewApiVersionSet()
    .HasApiVersion(1.0)
    .ReportApiVersions()
    .Build();

var versionGroup = app
    .MapGroup("/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

versionGroup.MapPagesEndpoints();
versionGroup.MapCacheInvalidationEndpoints();

if (app.Environment.IsLocal())
{
    app
        .MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)))
        .WithTags("Debug");
}

var version = AssemblyVersionExtensions.GetVersion();
app
    .MapGet("/version", () => new { version })
    .WithTags("Version");

await app.RunAsync();
