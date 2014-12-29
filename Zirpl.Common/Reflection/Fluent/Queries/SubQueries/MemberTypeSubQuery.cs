using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal class MemberTypeSubQuery : SubQueryBase<MemberInfo, IMemberQuery>,
        IMemberTypeQuery
    {
        private readonly MemberTypeEvaluator _memberTypeEvaluator;

        internal MemberTypeSubQuery(IMemberQuery returnQuery, MemberTypeEvaluator memberTypeEvaluator)
            :base(returnQuery)
        {
            _memberTypeEvaluator = memberTypeEvaluator;
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
            return _returnQuery;
        }

        IMemberQuery IMemberTypeQuery.And()
        {
            return _returnQuery;
        }
    }
}
