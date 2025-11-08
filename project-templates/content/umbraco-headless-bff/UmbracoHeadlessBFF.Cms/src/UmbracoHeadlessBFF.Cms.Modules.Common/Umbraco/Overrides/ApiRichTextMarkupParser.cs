using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.DeliveryApi;
using Umbraco.Cms.Core.PublishedCache;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Umbraco.Overrides;

internal sealed partial class ApiRichTextMarkupParser : IApiRichTextMarkupParser
{
    private readonly IPublishedContentCache _publishedContentCache;
    private readonly IPublishedMediaCache _publishedMediaCache;
    private readonly IApiMediaUrlProvider _apiMediaUrlProvider;
    private readonly ILogger<ApiRichTextMarkupParser> _logger;

    public ApiRichTextMarkupParser(IPublishedContentCache publishedContentCache,
        IPublishedMediaCache publishedMediaCache,
        IApiMediaUrlProvider apiMediaUrlProvider,
        ILogger<ApiRichTextMarkupParser> logger)
    {
        _publishedContentCache = publishedContentCache;
        _publishedMediaCache = publishedMediaCache;
        _apiMediaUrlProvider = apiMediaUrlProvider;
        _logger = logger;
    }

    [GeneratedRegex("{localLink:(?<guid>.+)}")]
    private static partial Regex UmbracoNodeLinkRegex();

    public string Parse(string html)
    {
        try
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            ProcessLinks(doc, _publishedContentCache, _publishedMediaCache);
            ProcessMedia(doc, _publishedMediaCache);

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

    private void ProcessLinks(HtmlDocument doc, IPublishedContentCache publishedContentCache, IPublishedMediaCache publishedMediaCache)
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
            var type = link.GetAttributeValue("type", "unknown");
            if (match.Success && Guid.TryParse(match.Groups["guid"].Value, out var guid))
            {
                switch (type)
                {
                    case Constants.UdiEntityType.Document:
                        link.SetAttributeValue("data-content-id", guid.ToString());
                        link.SetAttributeValue("data-entity-type", Constants.UdiEntityType.Document);
                        link.Attributes.Remove("href");
                        break;
                    case Constants.UdiEntityType.Media:
                        var media = publishedMediaCache.GetById(guid);
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

    private void ProcessMedia(HtmlDocument doc, IPublishedMediaCache publishedMediaCache)
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

            if (!UdiParser.TryParse(dataUdi, out var udiValue) || udiValue is not GuidUdi guidUdi)
            {
                continue;
            }

            var media = publishedMediaCache.GetById(guidUdi.Guid);
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
