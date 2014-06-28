using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Zirpl.Collections
{
    public static class IListExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (range != null)
            {
                foreach (T t in range)
                {
                    collection.Add(t);
                }
            }
        }
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable range)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (range != null)
            {
                foreach (T t in range)
                {
                    collection.Add(t);
                }
            }
        }

        public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> match)
        {
            var objsToRemove = collection.Where(o => match.Invoke(o)).ToList();
            foreach (var obj in objsToRemove)
            {
                collection.Remove(obj);
            }
        }


        public static void AddRange<T>(this IList collection, IEnumerable<T> range)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (range != null)
            {
                foreach (var t in range)
                {
                    collection.Add(t);
                }
            }
        }
        public static void AddRange(this IList collection, IEnumerable range)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (range != null)
            {
                foreach (var t in range)
                {
                    collection.Add(t);
                }
            }
        }

        public static void RemoveAll(this IList collection, Func<Object, bool> match)
        {
            var objsToRemove = collection.OfType<Object>().Where(o => match.Invoke(o)).ToList();
            foreach (var obj in objsToRemove)
            {
                collection.Remove(obj);
            }
        }
    }
}
