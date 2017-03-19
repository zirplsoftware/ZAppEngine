using System;
using System.Collections;
using System.Collections.Generic;

namespace Zirpl.AppEngine.VisualStudioAutomation.Utilities
{
    internal static class ActionExtensions
    {
        internal static ActionRunner GetRunner(this Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            return new ActionRunner(action);
        }
        internal static void ForEach<T>(this Action<T> action, IEnumerable<T> enumerable)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    action(item);
                }
            }
        }
        internal static void ForEach<T>(this Action<T> action, IEnumerable enumerable)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    action((T) item);
                }
            }
        }
    }
}
