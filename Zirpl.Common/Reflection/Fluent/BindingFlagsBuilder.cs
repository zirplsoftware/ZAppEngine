using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class BindingFlagsBuilder
    {
        internal bool Public { get; set; }
        internal bool Instance { get; set; }
        internal bool Static { get; set; }
        internal bool NonPublic { get; set; }
        internal bool FlattenHeirarchy { get; set; }
        internal bool DeclaredOnly { get; set; }
        internal bool IgnoreCase { get; set; }

        internal BindingFlags BindingFlags
        {
            get
            {
                if (DeclaredOnly && FlattenHeirarchy) throw new InvalidOperationException("Both DeclaredOnly and FlattenHeirarchy are true");

                // default
#if PORTABLE
                var bindings = Public ? BindingFlags.Public : default(BindingFlags);
#else
                var bindings = BindingFlags.Default;
                // now adjust according to the specs
                bindings = Public ? bindings | BindingFlags.Public : bindings;
#endif
                bindings = NonPublic ? bindings | BindingFlags.NonPublic : bindings;
                bindings = Instance ? bindings | BindingFlags.Instance : bindings;
                bindings = Static ? bindings | BindingFlags.Static : bindings;
                bindings = FlattenHeirarchy ? bindings | BindingFlags.FlattenHierarchy : bindings;
                bindings = DeclaredOnly ? bindings | BindingFlags.DeclaredOnly : bindings;
                bindings = IgnoreCase ? bindings | BindingFlags.IgnoreCase : bindings;
                // allow derived class to do further adjustments
                //bindings = AdjustBindings(bindings);
                return bindings;
            }
        }
    }
}
