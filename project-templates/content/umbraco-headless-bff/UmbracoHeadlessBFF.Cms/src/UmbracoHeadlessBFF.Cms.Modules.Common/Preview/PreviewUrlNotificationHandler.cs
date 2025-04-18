using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Security;
using UmbracoHeadlessBFF.Cms.Modules.Common.SiteResolution;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

internal sealed class PreviewUrlNotificationHandler : INotificationHandler<SendingContentNotification>
{
    private readonly ILinkService _linkService;
    private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
    private readonly PreviewConfiguration _previewConfiguration;
    private readonly ILogger<PreviewUrlNotificationHandler> _logger;

    public PreviewUrlNotificationHandler(ILinkService linkService, IBackOfficeSecurityAccessor backOfficeSecurityAccessor,
        IOptions<PreviewConfiguration> previewConfiguration, ILogger<PreviewUrlNotificationHandler> logger)
    {
        _linkService = linkService;
        _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        _logger = logger;
        _previewConfiguration = previewConfiguration.Value;
    }

    public void Handle(SendingContentNotification notification)
    {
        if (string.IsNullOrWhiteSpace(_previewConfiguration.SecretKey) || Encoding.UTF8.GetByteCount(_previewConfiguration.SecretKey) < 512)
        {
            _logger.LogWarning("No preview secret configured or is under required 512 byte length, skipping headless preview URL generation");
            return;
        }

        var previewUrls = new List<NamedUrl>();

        foreach (var variant in notification.Content.Variants)
        {
            if (notification.Content.Key is null || variant.Language?.IsoCode is null)
            {
                continue;
            }

            var link = _linkService.GetLinkByContentId(notification.Content.Key.Value, variant.Language.IsoCode, true);

            if (link is null)
            {
                continue;
            }

            var domain = link.Authority.StartsWith("http") ? link.Authority.Replace("http:", "https:") : $"https://{link.Authority}";

            var uriBuilder = new UriBuilder(domain)
            {
                Path = link.Path,
                Query = $"?previewMode=true&previewToken={GenerateToken()}"
            };

            previewUrls.Add(new NamedUrl
            {
                Name = $"Headless Preview - {variant.Language.Name}",
                Url = uriBuilder.Uri.ToString()
            });
        }

        foreach (var variant in notification.Content.Variants)
        {
            variant.AdditionalPreviewUrls = previewUrls;
        }
    }

    private string GenerateToken()
    {
        var user = _backOfficeSecurityAccessor.BackOfficeSecurity?.CurrentUser;

        if (user is null)
        {
            return string.Empty;
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_previewConfiguration.SecretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new([new(ClaimTypes.Name, "UmbracoHeadlessBFF-preview")]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credentials
        };

        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(handler.CreateToken(tokenDescriptor));
    }
}
