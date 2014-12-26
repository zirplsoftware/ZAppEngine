using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    [Flags]
    internal enum MemberTypeFlags
    {
        All = 0xbf,
        Constructor = 1,
        Custom = 0x40,
        Event = 2,
        Field = 4,
        Method = 8,
        NestedType = 0x80,
        Property = 0x10,
        TypeInfo = 0x20
    }
}
