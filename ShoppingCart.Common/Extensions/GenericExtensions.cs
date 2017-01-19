using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Common.Extensions
{
    public static class GenericExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
        {
            return collection ?? Enumerable.Empty<T>();
        }

        public static bool EmptyIfNullBoolean<T>(this IEnumerable<T> collection)
        {
            return collection == null;
        }
    }
}