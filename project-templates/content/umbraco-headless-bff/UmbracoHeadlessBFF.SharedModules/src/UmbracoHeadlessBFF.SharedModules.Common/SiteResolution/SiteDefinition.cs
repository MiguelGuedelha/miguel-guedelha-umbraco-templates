namespace UmbracoHeadlessBFF.SharedModules.Common.SiteResolution;

public sealed record SiteDefinition
{
    public required string Instance { get; init; }
    public required Guid RootId { get; init; }
    public required Guid SiteSettingsId { get; init; }
    public required Guid DictionaryId { get; init; }
    public Guid HomepageId { get; init; }
    public required string ThemeId { get; init; }
    public required string CultureInfo { get; init; }
    public required string Domain { get; init; }
    public required string Path { get; init; }
    public required string BasePath { get; init; }
    public Guid? NotFoundPageId { get; set; }
}
