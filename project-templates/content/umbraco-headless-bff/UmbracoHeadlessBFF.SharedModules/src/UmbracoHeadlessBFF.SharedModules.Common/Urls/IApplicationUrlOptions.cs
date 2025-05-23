namespace UmbracoHeadlessBFF.SharedModules.Common.Urls;

public interface IApplicationUrlOptions
{
    public const string SectionName = "ApplicationUrls";
    string Media { get; init; }
}
