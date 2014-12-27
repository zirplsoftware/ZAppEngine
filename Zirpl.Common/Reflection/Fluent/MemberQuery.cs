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

        internal MemberQuery(Type type)
            :base(type)
        {
        }

        public IMemberTypeQuery OfMemberType()
        {
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Constructor()
        {
            _memberTypesBuilder.Constructor = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Event()
        {
            _memberTypesBuilder.Event = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Field()
        {
            _memberTypesBuilder.Field = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Method()
        {
            _memberTypesBuilder.Method = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.NestedType()
        {
            _memberTypesBuilder.NestedType = true;
            return this;
        }

        IMemberTypeQuery IMemberTypeQuery.Property()
        {
            _memberTypesBuilder.Property = true;
            return this;
        }

        IMemberQuery IMemberTypeQuery.All()
        {
            _memberTypesBuilder.Constructor = true;
            _memberTypesBuilder.Event = true;
            _memberTypesBuilder.Field = true;
            _memberTypesBuilder.Method = true;
            _memberTypesBuilder.NestedType = true;
            _memberTypesBuilder.Property = true;
            return this;
        }

        IMemberQuery IMemberTypeQuery.And()
        {
            return this;
        }
    }
}
