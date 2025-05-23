using System.Collections.Frozen;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;

public sealed record ContentFetchType
{
    private static readonly FrozenDictionary<Options, string> s_optionsStringMap = new Dictionary<Options, string>
    {
        { Options.Children, "children" },
        { Options.Descendants, "descendants" },
        { Options.Ancestors, "ancestors" }
    }.ToFrozenDictionary();

    public required string IdOrPath { get; init; }
    public Options FetchType { get; init; }

    public override string ToString()
    {
        return $"{s_optionsStringMap[FetchType]}:{IdOrPath}";
    }

    public enum Options
    {
        Children,
        Descendants,
        Ancestors
    }
}
