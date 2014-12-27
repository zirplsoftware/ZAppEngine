using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MemberQuery : MemberQueryBase<MemberInfo, IMemberQuery, IMemberAccessibilityQuery, IMemberScopeQuery>,
        IMemberQuery,
        IMemberAccessibilityQuery,
        IMemberScopeQuery
    {
        internal MemberQuery(Type type)
            :base(type)
        {
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return true;
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.All; }
        }
    }
}
