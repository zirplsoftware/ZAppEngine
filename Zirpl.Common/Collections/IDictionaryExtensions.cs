using System;
using System.Collections;
using System.Collections.Generic;

namespace Zirpl.Collections
{
    public static class IDictionaryExtensions
    {
        public static IDictionary<TKey, TValue> OfType<TKey, TValue>(this IDictionary dictionary)
        {
            var convertedDictionary = new Dictionary<TKey, TValue>();

            IDictionaryEnumerator enumerator = dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                convertedDictionary.Add((TKey)enumerator.Key, (TValue)enumerator.Value);
            }

            return convertedDictionary;
        }
        public static IDictionary<TNewKey, TNewValue> OfType<TNewKey, TNewValue, TOldKey, TOldValue>(this IDictionary<TOldKey, TOldValue> dictionary)
        {
            var convertedDictionary = new Dictionary<TNewKey, TNewValue>();

            var enumerator = dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                convertedDictionary.Add((TNewKey)((Object)enumerator.Current.Key), (TNewValue)((Object)enumerator.Current.Value));
            }

            return convertedDictionary;
        }
    }
}
