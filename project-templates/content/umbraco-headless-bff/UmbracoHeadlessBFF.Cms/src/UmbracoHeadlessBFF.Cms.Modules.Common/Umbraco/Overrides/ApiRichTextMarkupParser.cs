using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Extensions;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Overrides;

internal sealed partial class ApiRichTextMarkupParser : IApiRichTextMarkupParser
{
    private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;
    private readonly IApiMediaUrlProvider _apiMediaUrlProvider;
    private readonly ILogger<ApiRichTextMarkupParser> _logger;

    [GeneratedRegex("{localLink:(?<udi>umb:.+)}")]
    private static partial Regex UmbracoNodeLinkRegex();

    public ApiRichTextMarkupParser(ILogger<ApiRichTextMarkupParser> logger,
        IPublishedSnapshotAccessor publishedSnapshotAccessor, IApiMediaUrlProvider apiMediaUrlProvider)
    {
        _logger = logger;
        _publishedSnapshotAccessor = publishedSnapshotAccessor;
        _apiMediaUrlProvider = apiMediaUrlProvider;
    }

    public string Parse(string html)
    {
        try
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var publishedSnapshot = _publishedSnapshotAccessor.GetRequiredPublishedSnapshot();

            ProcessLinks(doc, publishedSnapshot);
            ProcessMedia(doc, publishedSnapshot);

            return doc.DocumentNode.InnerHtml;
        }
        catch (Exception ex)
        {
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError(ex, "Could not parse rich text HTML, see exception for details");
            }
            return html;
        }
    }

    private void ProcessLinks(HtmlDocument doc, IPublishedSnapshot publishedSnapshot)
    {
        var links = doc.DocumentNode.SelectNodes("//a");

        if (links is null)
        {
            return;
        }

        foreach (var link in links)
        {
            link.Attributes.Remove("title");
            var href = link.GetAttributeValue("href", string.Empty);

            if (string.IsNullOrWhiteSpace(href))
            {
                continue;
            }

            var match = UmbracoNodeLinkRegex().Match(href);
            var handled = false;
            if (match.Success && UdiParser.TryParse(match.Groups["udi"].Value, out var udi) && udi is GuidUdi guidUdi)
            {
                switch (guidUdi.EntityType)
                {
                    case Constants.UdiEntityType.Document:
                        link.SetAttributeValue("data-content-id", guidUdi.Guid.ToString());
                        link.SetAttributeValue("data-entity-type", Constants.UdiEntityType.Document);
                        link.Attributes.Remove("href");
                        break;
                    case Constants.UdiEntityType.Media:
                        var media = publishedSnapshot.Media?.GetById(guidUdi.Guid);
                        if (media is not null)
                        {
                            link.SetAttributeValue("href", _apiMediaUrlProvider.GetUrl(media));
                            link.SetAttributeValue("data-entity-type", Constants.UdiEntityType.Media);
                            handled = true;
                        }
                        break;
                }

                if (!handled)
                {
                    link.Attributes.Remove("href");
                }
            }
            else
            {
                link.Attributes.Remove("data-anchor");
            }
        }
    }

    private void ProcessMedia(HtmlDocument doc, IPublishedSnapshot publishedSnapshot)
    {
        var images = doc.DocumentNode.SelectNodes("//img");

        if (images is null)
        {
            return;
        }

        foreach (var image in images)
        {
            var dataUdi = image.GetAttributeValue("data-udi", string.Empty);

            if (string.IsNullOrWhiteSpace(dataUdi))
            {
                continue;
            }

            if (!UdiParser.TryParse(dataUdi, out var udiValue))
            {
                continue;
            }

            var media = publishedSnapshot.Media?.GetById(udiValue);
            if (media is null)
            {
                continue;
            }

            var url = _apiMediaUrlProvider.GetUrl(media);

            var currentImageSource = image.GetAttributeValue("src", string.Empty);
            var currentImageQueryString = currentImageSource.Contains('?')
                ? $"?{currentImageSource.Split('?').Last()}"
                : null;

            image.SetAttributeValue("src", $"{url}{currentImageQueryString}");
            image.Attributes.Remove("data-udi");

            image.Attributes.Remove("data-caption");
        }
    }
}
