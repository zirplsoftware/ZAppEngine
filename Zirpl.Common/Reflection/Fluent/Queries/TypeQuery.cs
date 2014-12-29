using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.Reflection.Fluent
{
    internal sealed class AssemblyTypeQuery : ITypeQuery
    {
        private readonly IList<Assembly> _assemblyList;
        private readonly TypeEvaluator _typeEvaluator;

        internal AssemblyTypeQuery(Assembly assembly)
        {
            _assemblyList = new List<Assembly>();
            _assemblyList.Add(assembly);
            _typeEvaluator = new TypeEvaluator();
        }
#if !PORTABLE
        internal AssemblyTypeQuery(AppDomain appDomain)
        {
            foreach (var assembly in appDomain.GetAssemblies())
            {
                _assemblyList.Add(assembly);
            }
            _typeEvaluator = new TypeEvaluator();
        }
#endif

        #region IQueryResult implementation
        
        IEnumerable<Type> IQueryResult<Type>.Execute()
        {
            var list = new List<Type>();
            var matches = from assembly in _assemblyList
                from type in assembly.GetTypes()
                where _typeEvaluator.IsMatch(type)
                select type;
            list.AddRange(matches);
            return list;
        }

        Type IQueryResult<Type>.ExecuteSingle()
        {
            var result = ((IQueryResult<Type>)this).Execute();
            if (result.Count() > 1) throw new AmbiguousMatchException("Found more than 1 member matching the criteria");

            return result.Single();
        }

        Type IQueryResult<Type>.ExecuteSingleOrDefault()
        {
            var result = ((IQueryResult<Type>)this).Execute();
            if (result.Count() > 1) throw new AmbiguousMatchException("Found more than 1 member matching the criteria");

            return result.SingleOrDefault();
        }

        #endregion

        ITypeQuery ITypeQuery.AssignableFrom(Type type)
        {
            _typeEvaluator.AssignableFrom = type;
            return this;
        }

        ITypeQuery ITypeQuery.AssignableFrom<T>()
        {
            _typeEvaluator.AssignableFrom = typeof(T);
            return this;
        }

        ITypeQuery ITypeQuery.AssignableFromAll(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableFroms = types;
            return this;
        }

        ITypeQuery ITypeQuery.AssignableFromAny(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableFroms = types;
            _typeEvaluator.Any = true;
            return this;
        }

        ITypeQuery ITypeQuery.AssignableTo(Type type)
        {
            _typeEvaluator.AssignableTo = type;
            return this;
        }

        ITypeQuery ITypeQuery.AssignableTo<T>()
        {
            _typeEvaluator.AssignableTo = typeof(T);
            return this;
        }

        ITypeQuery ITypeQuery.AssignableToAll(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableTos = types;
            return this;
        }

        ITypeQuery ITypeQuery.AssignableToAny(IEnumerable<Type> types)
        {
            _typeEvaluator.AssignableTos = types;
            _typeEvaluator.Any = true;
            return this;
        }

        INameQuery<Type, ITypeQuery> ITypeQuery.Named()
        {
            return new NameSubQuery<Type, ITypeQuery>(this, _typeEvaluator.NameEvaluator);
        }

        INameQuery<Type, ITypeQuery> ITypeQuery.FullNamed()
        {
            return new NameSubQuery<Type, ITypeQuery>(this, _typeEvaluator.FullNameEvaluator);
        }
    }
}
