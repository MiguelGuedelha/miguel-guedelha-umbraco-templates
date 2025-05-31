namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi.Pages;

public sealed record ApiSiteDictionary : IApiContent<ApiSiteDictionaryProperties>
{
    public Guid Id { get; init; }
    public string ContentType => DeliveryApiConstants.ContentTypes.ApiSiteDictionary;
    public required string Name { get; init; }
    public DateTime CreateDate { get; init; }
    public DateTime UpdateDate { get; init; }
    public required ApiContentRoute Route { get; init; }
    public Dictionary<string, ApiContentRoute> Cultures { get; init; } = [];
    public required ApiSiteDictionaryProperties Properties { get; init; }
}

public sealed record ApiSiteDictionaryProperties
{
    public string? GeneralButtonsNextText { get; init; }
    public string? GeneralButtonsBackText { get; init; }
    public string? GeneralSearchPlaceholderText { get; init; }
}
