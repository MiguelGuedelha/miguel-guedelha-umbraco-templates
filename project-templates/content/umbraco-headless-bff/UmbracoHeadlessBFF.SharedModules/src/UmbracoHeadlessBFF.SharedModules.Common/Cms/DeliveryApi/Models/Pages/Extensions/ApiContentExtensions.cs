using System.Diagnostics.CodeAnalysis;

namespace UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Pages.Extensions;

public static class ApiContentExtensions
{
    public static bool TryGetProperties<TOut>(this IApiContent content, [NotNullWhen(true)]out TOut? outValue)
        where TOut : class
    {
        outValue = null;
        if (content.GetType().GetProperty("Properties")?.GetValue(content) is not TOut properties)
        {
            return false;
        }

        outValue = properties;
        return true;
    }
}
