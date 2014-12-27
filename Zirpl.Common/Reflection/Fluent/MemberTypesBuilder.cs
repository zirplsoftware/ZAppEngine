using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zirpl.Enums;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MemberTypesBuilder
    {
        internal bool Constructor { get; set; }
        internal bool Event { get; set; }
        internal bool Field { get; set; }
        internal bool Method { get; set; }
        internal bool NestedType { get; set; }
        internal bool Property { get; set; }

        internal MemberTypeFlags MemberTypes
        {
            get
            {
                var memberTypes = default(MemberTypeFlags);
                if (Constructor
                    || Event
                    || Field
                    || Method
                    || NestedType
                    || Property)
                {
                    if (Constructor) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Constructor);
                    if (Event) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Event);
                    if (Field) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Field);
                    if (Method) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Method);
                    if (NestedType) memberTypes = memberTypes.AddFlag(MemberTypeFlags.NestedType);
                    if (Property) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Property);
                }
                else
                {
                    // default to ALL 
                    memberTypes = MemberTypeFlags.AllExceptCustomAndTypeInfo;
                }
                return memberTypes;
            }
        }
    }
}
