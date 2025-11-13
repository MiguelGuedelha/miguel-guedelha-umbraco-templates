namespace UmbracoHeadlessBFF.SiteApi.Modules.Pages.Models.Dictionary;

internal sealed record GeneralLabels
{
    public required SearchLabels Search { get; init; }
    public required ButtonLabels Buttons { get; init; }
}

internal sealed record SearchLabels
{
    public required string PlaceholderText { get; init; }
}

internal sealed record ButtonLabels
{
    public required string Next { get; init; }
    public required string Back { get; init; }
}
