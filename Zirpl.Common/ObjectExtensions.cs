using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl
{
    public static class ObjectExtensions
    {
        public static T Or<T>(this T obj, T replacement)
        {
            return obj != null ? obj : replacement;
        }
    }
}
