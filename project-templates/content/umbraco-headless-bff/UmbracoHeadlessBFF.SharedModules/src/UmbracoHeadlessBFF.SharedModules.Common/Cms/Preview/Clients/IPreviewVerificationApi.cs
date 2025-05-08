using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Preview.Clients;

public interface IPreviewVerificationApi
{
    [Get("/verify")]
    Task<IApiResponse> VerifyPreviewMode([Header("Authorization")] string token);
}
