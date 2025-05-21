namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Models;

public sealed record Link
{
    public required string Authority { get; init; }
    public required string Path { get; init; }
}
