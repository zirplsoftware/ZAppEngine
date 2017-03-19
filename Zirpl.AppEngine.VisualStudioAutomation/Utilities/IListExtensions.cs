﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Zirpl.AppEngine.VisualStudioAutomation.Utilities
{
    internal static class IListExtensions
    {
        internal static void AddRange<T>(this IList list, IEnumerable<T> range)
        {
            if (list == null) throw new ArgumentNullException("list");

            if (range == null) return;

            foreach (T t in range)
            {
                list.Add(t);
            }
        }
        internal static void AddRange(this IList list, IEnumerable range)
        {
            if (list == null) throw new ArgumentNullException("list");

            if (range == null) return;

            foreach (var t in range)
            {
                list.Add(t);
            }
        }
        internal static void AddRange<T>(this IList<T> list, IEnumerable<T> range)
        {
            if (list == null) throw new ArgumentNullException("list");

            if (range == null) return;

            foreach (T t in range)
            {
                list.Add(t);
            }
        }
        internal static void AddRange<T>(this IList<T> list, IEnumerable range)
        {
            if (list == null) throw new ArgumentNullException("list");

            if (range == null) return;

            foreach (var t in range)
            {
                list.Add((T)t);
            }
        }

        internal static void RemoveWhere(this IList list, Func<Object, bool> match)
        {
            var objsToRemove = list.OfType<Object>().Where(match.Invoke).ToList();
            foreach (var obj in objsToRemove)
            {
                list.Remove(obj);
            }
        }

        internal static void RemoveWhere<T>(this IList list, Func<T, bool> match)
        {
            var objsToRemove = list.OfType<T>().Where(match.Invoke).ToList();
            foreach (var obj in objsToRemove)
            {
                list.Remove(obj);
            }
        }
    }
}