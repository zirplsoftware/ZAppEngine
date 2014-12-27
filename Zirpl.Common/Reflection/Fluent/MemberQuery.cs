using System;
using System.Reflection;
using Zirpl.Enums;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MemberQuery : MemberQueryBase<MemberInfo, IMemberQuery, IMemberAccessibilityQuery, IMemberScopeQuery>,
        IMemberQuery,
        IMemberAccessibilityQuery,
        IMemberScopeQuery,
        IMemberTypeQuery
    {
        private readonly MemberTypeFlagsBuilder _memberTypeFlagsBuilder;

        internal MemberQuery(Type type)
            :base(type)
        {
            _memberTypeFlagsBuilder = new MemberTypeFlagsBuilder();
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return _memberTypeFlagsBuilder.IsMatch(memberInfo);
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return _memberTypeFlagsBuilder.MemberTypes; }
        }

        public IMemberTypeQuery OfMemberType()
        {
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Constructor()
        {
            _memberTypeFlagsBuilder.Constructor = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Event()
        {
            _memberTypeFlagsBuilder.Event = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Field()
        {
            _memberTypeFlagsBuilder.Field = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Method()
        {
            _memberTypeFlagsBuilder.Method = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.NestedType()
        {
            _memberTypeFlagsBuilder.NestedType = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Property()
        {
            _memberTypeFlagsBuilder.Property = true;
            return this;
        }

        IMemberQuery IMemberTypeQuery.All()
        {
            _memberTypeFlagsBuilder.Constructor = true;
            _memberTypeFlagsBuilder.Event = true;
            _memberTypeFlagsBuilder.Field = true;
            _memberTypeFlagsBuilder.Method = true;
            _memberTypeFlagsBuilder.NestedType = true;
            _memberTypeFlagsBuilder.Property = true;
            return this;
        }

        IMemberQuery IMemberTypeQuery.And()
        {
            return this;
        }

        private class MemberTypeFlagsBuilder
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
                    if (Constructor) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Constructor);
                    if (Event) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Event);
                    if (Field) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Field);
                    if (Method) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Method);
                    if (NestedType) memberTypes = memberTypes.AddFlag(MemberTypeFlags.NestedType);
                    if (Property) memberTypes = memberTypes.AddFlag(MemberTypeFlags.Property);
                    return memberTypes;
                }
            }

            internal bool IsMatch(MemberInfo member)
            {
                if (Constructor && member is ConstructorInfo) return true;
                if (Event && member is EventInfo) return true;
                if (Field && member is FieldInfo) return true;
                if (Method && member is MethodInfo) return true;
                if (NestedType && member is Type) return true;
                if (Property && member is PropertyInfo) return true;
                return false;
            }
        }
    }
}
