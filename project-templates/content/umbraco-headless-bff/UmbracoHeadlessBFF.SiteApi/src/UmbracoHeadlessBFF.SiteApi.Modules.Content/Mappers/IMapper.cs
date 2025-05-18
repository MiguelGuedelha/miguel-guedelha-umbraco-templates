using UmbracoHeadlessBFF.SharedModules.Common.Cms.DeliveryApi.Models.Data.Abstractions;
using UmbracoHeadlessBFF.SiteApi.Modules.Content.Models.Components.Abstractions;

namespace UmbracoHeadlessBFF.SiteApi.Modules.Content.Mappers;

internal interface IMapper<in TIn, TOut>
{
    Task<TOut?> Map(TIn model);
}

internal interface IComponentMapper : IMapper<IApiElement, IComponent>
{
    bool CanMap(string type);
}

internal static class MapperExtensions
{
    private const string ErrorMessage = "Invalid conversion of {0} to {1}";

    public static TOut ToOrThrow<TOut>(this object source, string message = "")
    {
        if (source is not TOut outValue)
        {
            throw new InvalidOperationException(string.Format(ErrorMessage, source.GetType(), typeof(TOut)));
        }

        return outValue;
    }
}
