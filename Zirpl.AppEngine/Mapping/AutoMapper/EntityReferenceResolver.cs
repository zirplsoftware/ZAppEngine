#if !NET35 && !NET35CLIENT
using AutoMapper;
using System;
using Zirpl.AppEngine.Ioc;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Service;

namespace Zirpl.AppEngine.Mapping.AutoMapper
{
    public class EntityReferenceResolver<TSource, TEntityProperty, TId> : ValueResolverBase<TSource, Object, TEntityProperty>
        where TEntityProperty : class
    {
        private Func<TSource, TId> GetEntityIdFunction { get; set; }
        private IDependencyResolver DependencyResolver { get; set; }

        public EntityReferenceResolver(IDependencyResolver dependencyResolver, Func<TSource, TId> getEntityIdFunction)
        {
            this.GetEntityIdFunction = getEntityIdFunction;
            this.DependencyResolver = dependencyResolver;
        }

        protected override TEntityProperty ResolveCore(ResolutionResult source, TSource contextSource, Object contextDestination)
        {
            TEntityProperty result = null;
            TId id = this.GetEntityIdFunction(contextSource);
            if (id.IsNullId())
            {
                result = null;
            }
            else
            {
                var supportsGets = DependencyResolver.Resolve<ISupportsGetById<TEntityProperty, TId>>();
                result = supportsGets.Get(id);
            }
            return result;
        }
    }
}
#endif