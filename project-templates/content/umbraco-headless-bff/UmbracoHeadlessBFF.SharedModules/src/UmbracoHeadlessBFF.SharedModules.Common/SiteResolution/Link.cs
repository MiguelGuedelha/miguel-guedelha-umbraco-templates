namespace UmbracoHeadlessBFF.SharedModules.Common.SiteResolution;

public sealed record Link
{
    public required string Authority { get; init; }
    public required string Path { get; init; }
}
