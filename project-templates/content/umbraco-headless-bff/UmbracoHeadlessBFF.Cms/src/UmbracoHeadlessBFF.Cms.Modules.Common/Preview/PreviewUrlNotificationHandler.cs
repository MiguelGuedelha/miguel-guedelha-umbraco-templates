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
using UmbracoHeadlessBFF.Cms.Modules.Common.Links;

namespace UmbracoHeadlessBFF.Cms.Modules.Common.Preview;

internal sealed class PreviewUrlNotificationHandler : INotificationHandler<SendingContentNotification>
{
    private readonly LinkService _linkService;
    private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
    private readonly PreviewOptions _previewOptions;
    private readonly ILogger<PreviewUrlNotificationHandler> _logger;

    public PreviewUrlNotificationHandler(LinkService linkService, IBackOfficeSecurityAccessor backOfficeSecurityAccessor,
        IOptions<PreviewOptions> previewConfiguration, ILogger<PreviewUrlNotificationHandler> logger)
    {
        _linkService = linkService;
        _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        _logger = logger;
        _previewOptions = previewConfiguration.Value;
    }

    public void Handle(SendingContentNotification notification)
    {
        if (string.IsNullOrWhiteSpace(_previewOptions.SecretKey) || Encoding.UTF8.GetByteCount(_previewOptions.SecretKey) < 512)
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

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_previewOptions.SecretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([new Claim(ClaimTypes.Name, "stada-preview")]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credentials
        };

        var handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(handler.CreateToken(tokenDescriptor));
    }
}
