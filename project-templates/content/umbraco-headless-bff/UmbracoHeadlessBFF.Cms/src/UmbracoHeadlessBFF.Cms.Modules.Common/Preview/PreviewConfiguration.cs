namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

internal class PreviewConfiguration
{
    public const string SectionName = "PreviewMode";
    public required string SecretKey { get; set; }
}
