using Zirpl.AppEngine.Ioc;

namespace Zirpl.AppEngine.Mapping.AutoMapper
{
    public abstract class MappingModuleBase
    {
        //private Dictionary<Type, Dictionary<Type, Object>> mappingExpressions;

        //protected MappingModuleBase()
        //{
        //    this.mappingExpressions = new Dictionary<Type, Dictionary<Type, object>>();
        //}

        //protected virtual IMappingExpression<TSource, TDestination> Cache<TSource, TDestination>(IMappingExpression<TSource, TDestination> mappingExpression)
        //{
        //    //var mappingExpression = Mapper.CreateMap<TSource, TDestination>();
        //    var sourceType = typeof(TSource);
        //    var destinationType = typeof(TDestination);
        //    if (!mappingExpressions.ContainsKey(sourceType))
        //    {
        //        mappingExpressions.Add(sourceType, new Dictionary<Type, Object>());
        //    }
        //    if (mappingExpressions[sourceType].ContainsKey(destinationType))
        //    {
        //        throw new Exception("This pair has already been mapped");
        //    }
        //    mappingExpressions[sourceType].Add(destinationType, mappingExpression);
        //    return mappingExpression;
        //}

        //protected virtual IMappingExpression<TSource, TDestination> GetCached<TSource, TDestination>()
        //{
        //    var sourceType = typeof(TSource);
        //    var destinationType = typeof(TDestination);
        //    return (IMappingExpression<TSource, TDestination>)this.mappingExpressions[sourceType][destinationType];
        //}

        public abstract void CreateMaps(IDependencyResolver dependencyResolver);
    }
}