using System;
using System.Collections;
using System.Collections.Generic;

namespace Zirpl.Collections
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
            return enumerable;
        }
        public static IEnumerable ForEach<T>(this IEnumerable enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action((T)item);
            }
            return enumerable;
        }
    }
}
