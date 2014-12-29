using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class MemberScopeSubQuery<TMemberInfo, TReturnQuery> : SubQueryBase<TMemberInfo, TReturnQuery>,
        IMemberScopeQuery<TMemberInfo, TReturnQuery>
        where TMemberInfo : MemberInfo
        where TReturnQuery : IQueryResult<TMemberInfo>
    {
        private readonly MemberScopeEvaluator _scopeEvaluator;

        internal MemberScopeSubQuery(TReturnQuery returnQuery, MemberScopeEvaluator scopeEvaluator)
            :base(returnQuery)
        {
            _scopeEvaluator = scopeEvaluator;
        }

        IMemberScopeQuery<TMemberInfo, TReturnQuery> IMemberScopeQuery<TMemberInfo, TReturnQuery>.Instance()
        {
            _scopeEvaluator.Instance = true;
            return this;
        }

        IMemberScopeQuery<TMemberInfo, TReturnQuery> IMemberScopeQuery<TMemberInfo, TReturnQuery>.Static()
        {
            _scopeEvaluator.Static = true;
            return this;
        }

        IMemberScopeQuery<TMemberInfo, TReturnQuery> IMemberScopeQuery<TMemberInfo, TReturnQuery>.DeclaredOnThisType()
        {
            _scopeEvaluator.DeclaredOnThisType = true;
            return this;
        }

        IMemberScopeQuery<TMemberInfo, TReturnQuery> IMemberScopeQuery<TMemberInfo, TReturnQuery>.DeclaredOnBaseTypes()
        {
            _scopeEvaluator.DeclaredOnBaseTypes = true;
            return this;
        }

        IMemberScopeQuery<TMemberInfo, TReturnQuery> IMemberScopeQuery<TMemberInfo, TReturnQuery>.DeclaredOnBaseTypes(int levelsDeep)
        {
            _scopeEvaluator.DeclaredOnBaseTypes = true;
            _scopeEvaluator.LevelsDeep = levelsDeep;
            return this;
        }

        TReturnQuery IMemberScopeQuery<TMemberInfo, TReturnQuery>.All()
        {
            _scopeEvaluator.DeclaredOnThisType = true;
            _scopeEvaluator.DeclaredOnBaseTypes = true;
            return _returnQuery;
        }

        TReturnQuery IMemberScopeQuery<TMemberInfo, TReturnQuery>.All(int levelsDeep)
        {
            _scopeEvaluator.DeclaredOnThisType = true;
            _scopeEvaluator.DeclaredOnBaseTypes = true;
            _scopeEvaluator.LevelsDeep = levelsDeep;
            return _returnQuery;
        }

        TReturnQuery IMemberScopeQuery<TMemberInfo, TReturnQuery>.Default()
        {
            return _returnQuery;
        }

        TReturnQuery IMemberScopeQuery<TMemberInfo, TReturnQuery>.And()
        {
            return _returnQuery;
        }
    }
}
