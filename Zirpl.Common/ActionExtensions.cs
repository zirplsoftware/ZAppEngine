using System;
using System.Collections;
using System.Collections.Generic;

namespace Zirpl
{
    public static class ActionExtensions
    {
        public static ActionRunner GetRunner(this Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            return new ActionRunner(action);
        }
        public static void ForEach<T>(this Action<T> action, IEnumerable<T> enumerable)
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
        public static void ForEach<T>(this Action<T> action, IEnumerable enumerable)
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
