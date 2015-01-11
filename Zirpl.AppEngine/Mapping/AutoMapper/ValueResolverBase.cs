using AutoMapper;

namespace Zirpl.AppEngine.Mapping.AutoMapper
{
    public abstract class ValueResolverBase<TSource, TDestination, TProperty> : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            return source.New(this.ResolveCore(source, (TSource)source.Value, (TDestination)source.Context.DestinationValue), typeof(TProperty));
        }

        protected abstract TProperty ResolveCore(ResolutionResult source, TSource contextSource, TDestination contextDestination);
    }
}
