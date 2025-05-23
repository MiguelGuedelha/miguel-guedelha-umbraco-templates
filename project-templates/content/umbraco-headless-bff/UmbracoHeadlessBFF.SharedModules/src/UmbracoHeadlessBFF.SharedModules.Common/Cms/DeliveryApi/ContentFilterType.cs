using System.Collections.Frozen;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi;

public sealed record ContentFilterType
{
    private readonly string? _stringValue;
    private readonly bool _negate;
    private readonly Options _filterType;

    private readonly DateOnly? _dateOnlyValue;

    private const string SToStringTemplate = "{0}{1}{2}";

    private static readonly FrozenDictionary<Options, string> s_optionsStringMap = new Dictionary<Options, string>
    {
        { Options.ContentType, "contentType:" },
        { Options.Name, "name:" },
        { Options.CreateDateGreaterThan, "createDate>" },
        { Options.CreateDateGreaterThanOrEqual, "createDate>:" },
        { Options.CreateDateLessThan, "createDate<" },
        { Options.CreateDateLessThanOrEqual, "createDate<:" },
        { Options.UpdateDateGreaterThan, "updateDate>" },
        { Options.UpdateDateGreaterThanOrEqual, "updateDate>:" },
        { Options.UpdateDateLessThan, "updateDate<" },
        { Options.UpdateDateLessThanOrEqual, "updateDate<:" },
    }.ToFrozenDictionary();

    public ContentFilterType(Options filter, string value, bool negate = false)
    {
        if (filter is Options.ContentType or Options.Name)
        {
            _stringValue = value;
            _negate = negate;
            _filterType = filter;
            return;
        }

        throw new InvalidOperationException($"The string value constructor is only valid for the {Options.ContentType} and {Options.Name} filters.");
    }

    public ContentFilterType(Options filter, DateOnly value)
    {
        if (filter is Options.ContentType or Options.Name)
        {
            throw new InvalidOperationException($"The DateOnly value constructor is not valid for the {Options.ContentType} and {Options.Name} filters.");
        }

        _dateOnlyValue = value;
        _filterType = filter;
    }

    public override string ToString()
    {
        return string.Format(SToStringTemplate, s_optionsStringMap[_filterType], _negate ? '!' : string.Empty, _dateOnlyValue is null ? _stringValue : _dateOnlyValue);
    }

    public enum Options
    {
        ContentType,
        Name,
        CreateDateGreaterThan,
        CreateDateGreaterThanOrEqual,
        CreateDateLessThan,
        CreateDateLessThanOrEqual,
        UpdateDateGreaterThan,
        UpdateDateGreaterThanOrEqual,
        UpdateDateLessThan,
        UpdateDateLessThanOrEqual
    }
}
