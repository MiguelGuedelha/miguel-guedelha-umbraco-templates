namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

internal sealed class PreviewOptions
{
    public const string SectionName = "PreviewMode";
    public required string SecretKey { get; set; }
}
