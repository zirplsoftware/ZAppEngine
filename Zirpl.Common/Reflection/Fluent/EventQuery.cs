using System;
using System.Reflection;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class EventQuery : MemberQueryBase<EventInfo, IEventQuery, IEventAccessibilityQuery, IEventScopeQuery>, 
        IEventQuery,
        IEventAccessibilityQuery,
        IEventScopeQuery
    {
        internal EventQuery(Type type)
            :base(type)
        {
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return true;
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return MemberTypeFlags.Event; }
        }
    }
}
