namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Links.Contracts;

public sealed record Link
{
    public required string Authority { get; init; }
    public required string Path { get; init; }
}
