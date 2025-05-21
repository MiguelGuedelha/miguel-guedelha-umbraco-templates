namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Models;

public sealed record RedirectLink
{
    public required string Location { get; init; }
    public required int StatusCode { get; init; }
}
