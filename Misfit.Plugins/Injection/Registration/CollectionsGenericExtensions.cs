using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.Plugins.Injection.Registration
{
    internal static class CollectionsGenericExtensions
    {
  
        internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        internal static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionaryToMerge)
        {
            var result = new Dictionary<TKey, TValue>();
            var dictionaries = new List<IDictionary<TKey, TValue>>();

            foreach (var x in dictionaries.SelectMany(dict => dict))
            {
                result[x.Key] = x.Value;
            }

            return result;
        }
    }
}
