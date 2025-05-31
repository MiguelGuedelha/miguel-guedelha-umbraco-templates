namespace UmbracoHeadlessBFF.SharedModules.Common.Strings;

public static class StringExtensions
{
    private static readonly char[] s_uriTrimChars = ['/', '\\'];

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
