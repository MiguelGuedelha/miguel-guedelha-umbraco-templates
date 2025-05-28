using System.Text.RegularExpressions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Strings;

public static partial class StringExtensions
{
    private static readonly char[] s_uriTrimChars = ['/', '\\'];

    [GeneratedRegex("^/{2,}")]
    private static partial Regex LeadingSlashesRegex();

    public static string ReplaceFirst(this string text, string term, string replace)
    {
        var index = text.IndexOf(term, StringComparison.InvariantCultureIgnoreCase);

        return index < 0
            ? text
            : $"{text[..index]}{replace}{text[(index + term.Length)..]}";
    }

    public static string CombineUri(this string first, params string[] parts)
    {
        return parts is { Length: 0 }
            ? first
            : parts.Aggregate(first, (current, t) => $"{current.TrimEnd(s_uriTrimChars)}/{t.TrimStart(s_uriTrimChars)}");
    }


    public static string SanitisePathSlashes(this string path)
    {
        return path.Equals("/") ? path : $"/{path.Trim('/')}/";
    }
}
