using System.Text.RegularExpressions;

namespace UmbracoHeadlessBFF.SharedModules.Common.Strings;

public static partial class StringExtensions
{
    private static readonly char[] s_uriTrimChars = ['/', '\\'];

    public static string ReplaceFirst(this string text, string term, string replace)
    {
        var index = text.IndexOf(term, StringComparison.InvariantCultureIgnoreCase);

        return index < 0
            ? text
            : $"{text[..index]}{replace}{text[(index + term.Length)..]}";
    }

    public static string CombineUri(params string[] parts)
    {
        var uri = string.Empty;

        if (parts is { Length: 0 })
        {
            return uri;
        }

        uri = parts[0].TrimEnd(s_uriTrimChars);

        for (var i = 1; i < parts.Length; i++)
        {
            uri = $"{uri.TrimEnd(s_uriTrimChars)}/{parts[i].TrimStart(s_uriTrimChars)}";

        }

        return uri;
    }


    public static string SanitiseLeadingSlashes(this string path)
    {
        return !path.StartsWith('/')
            ? $"/{path}"
            : LeadingSlashesRegex().Replace(path, "/");
    }

    [GeneratedRegex("^/{2,}")]
    private static partial Regex LeadingSlashesRegex();
}
