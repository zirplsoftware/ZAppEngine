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
        private readonly MemberTypeEvaluator _memberTypeEvaluator;

        internal MemberQuery(Type type)
            :base(type)
        {
            _memberTypeEvaluator = new MemberTypeEvaluator();
        }

        protected override bool IsMatch(MemberInfo memberInfo)
        {
            return _memberTypeEvaluator.IsMatch(memberInfo);
        }

        protected override MemberTypeFlags MemberTypes
        {
            get { return _memberTypeEvaluator.MemberTypes; }
        }

        public IMemberTypeQuery OfMemberType()
        {
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Constructor()
        {
            _memberTypeEvaluator.Constructor = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Event()
        {
            _memberTypeEvaluator.Event = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Field()
        {
            _memberTypeEvaluator.Field = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Method()
        {
            _memberTypeEvaluator.Method = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.NestedType()
        {
            _memberTypeEvaluator.NestedType = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Property()
        {
            _memberTypeEvaluator.Property = true;
            return this;
        }

        IMemberQuery IMemberTypeQuery.All()
        {
            _memberTypeEvaluator.Constructor = true;
            _memberTypeEvaluator.Event = true;
            _memberTypeEvaluator.Field = true;
            _memberTypeEvaluator.Method = true;
            _memberTypeEvaluator.NestedType = true;
            _memberTypeEvaluator.Property = true;
            return this;
        }

        IMemberQuery IMemberTypeQuery.And()
        {
            return this;
        }

        private class MemberTypeEvaluator
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
