namespace UmbracoHeadlessBFF.SharedModules.Cms.SiteResolution;

public sealed record SiteDefinition
{
    public required Guid RootId { get; init; }
    public required Guid SiteSettingsId { get; init; }
    public required Guid DictionaryId { get; init; }
    public Guid HomepageId { get; init; }
    public required string CultureInfo { get; init; }
    public IReadOnlyCollection<SiteDefinitionDomain> Domains { get; init; } = [];
    public required string BasePath { get; init; }
    public Guid NotFoundPageId { get; set; }
    public Guid SearchPageId { get; set; }
}

public sealed record SiteDefinitionDomain
{
    public required string Scheme { get; init; }
    public required string Domain { get; init; }
    public required string Path { get; init; }
}
