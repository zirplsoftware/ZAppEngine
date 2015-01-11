using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Zirpl.Collections
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            if (range == null) return;

            foreach (T t in range)
            {
                collection.Add(t);
            }
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable range)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            if (range == null) return;

            foreach (T t in range)
            {
                collection.Add(t);
            }
        }

        public static void RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> match)
        {
            var objsToRemove = collection.Where(match.Invoke).ToList();
            foreach (var obj in objsToRemove)
            {
                collection.Remove(obj);
            }
        }
    }
}
