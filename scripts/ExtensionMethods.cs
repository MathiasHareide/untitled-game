using System.Collections.Generic;
using System.Linq;


namespace lamo
{
    public static class ExtensionMethods
    {
        public static bool NullorEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Any();
        }
    }
}