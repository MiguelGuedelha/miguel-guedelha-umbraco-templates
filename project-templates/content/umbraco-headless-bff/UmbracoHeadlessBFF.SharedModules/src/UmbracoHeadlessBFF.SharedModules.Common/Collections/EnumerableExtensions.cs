namespace UmbracoHeadlessBFF.SharedModules.Common.Collections;

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T?> source)
    {
        public IEnumerable<T> WhereNotNull()
        {
            ArgumentNullException.ThrowIfNull(source);

            return source.Where(x => x is not null)!;
        }
    }
}
