using System.Collections.Frozen;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;

public sealed record ContentSortType
{
    private static readonly FrozenDictionary<Options, string> s_optionsStringMap = new Dictionary<Options, string>
    {
        { Options.CreateDateAscending, "createDate:asc" },
        { Options.CreateDateDescending, "createDate:desc" },
        { Options.LevelAscending, "level:asc" },
        { Options.LevelDescending, "level:desc" },
        { Options.NameAscending, "name:asc" },
        { Options.NameDescending, "name:desc" },
        { Options.SortOrderAscending, "sortOrder:asc" },
        { Options.SortOrderDescending, "sortOrder:desc" },
        { Options.UpdateDateAscending, "updateDate:asc" },
        { Options.UpdateDateDescending, "updateDate:desc" }
    }.ToFrozenDictionary();

    public required Options SortType { get; init; }

    public override string ToString()
    {
        return s_optionsStringMap[SortType];
    }

    public enum Options
    {
        CreateDateAscending,
        CreateDateDescending,
        LevelAscending,
        LevelDescending,
        NameAscending,
        NameDescending,
        SortOrderAscending,
        SortOrderDescending,
        UpdateDateAscending,
        UpdateDateDescending
    }
}
