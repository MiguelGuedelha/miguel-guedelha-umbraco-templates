using Refit;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.Preview;

public interface IPreviewVerificationApi
{
    [Get("/verify")]
    Task<IApiResponse> VerifyPreviewMode([Header("Authorization")] string token);
}
