using System.Diagnostics;
using System.Text;

namespace UmbracoHeadlessBFF.SharedModules.Cms.DeliveryApi;

public sealed record FieldsExpandProperties
{
    private readonly IReadOnlyCollection<FieldsExpandSubProperties>? _children;
    private readonly int? _level;

    public FieldsExpandProperties(int level)
    {
        _level = level;
    }

    public FieldsExpandProperties(IReadOnlyCollection<FieldsExpandSubProperties> children)
    {
        _children = children;
    }

    public override string ToString()
    {
        if (_level is not >= 1)
        {
            return _children is { Count: > 0 }
                ? $"properties[{string.Join(",", _children.Select(x => x.ToString()))}]"
                : throw new UnreachableException("this should not happen");
        }

        const string basePart = "properties[$all";
        var builder = new StringBuilder();

        // Open brackets and inner base strings
        for (var i = 0; i < _level.Value - 1; i++)
        {
            builder.Append(basePart + "[");
        }

        // Final basePart without trailing bracket
        builder.Append(basePart);

        // Closing brackets
        builder.Append(new string(']', 1 + (_level.Value - 1) * 2));

        return builder.ToString();
    }
}

public sealed record FieldsExpandSubProperties
{
    public required string Name { get; init; }
    public IReadOnlyCollection<FieldsExpandSubProperties>? Children { get; init; }

    public override string ToString()
    {
        return $"{Name}{(Children is { Count: > 0 } ? $"[properties[{string.Join(",", Children.Select(x => x.ToString()))}]]" : "")}";
    }
}
