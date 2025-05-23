namespace UmbracoHeadlessBFF.SharedModules.Common.Urls;

public abstract class BaseApplicationUrlOptions
{
    public const string SectionName = "ApplicationUrls";
    public required string Media { get; init; }
}
