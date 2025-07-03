namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

internal sealed record PreviewOptions
{
    public const string SectionName = "PreviewMode";
    public required string SecretKey { get; init; }
}
