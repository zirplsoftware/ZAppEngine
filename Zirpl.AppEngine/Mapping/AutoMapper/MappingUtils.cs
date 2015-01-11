using AutoMapper;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Mapping.AutoMapper
{
    public static class MappingUtils
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAuditableDestinationFields<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
            where TDestination : IAuditable
        {
            expression.ForMember(entity => entity.CreatedDate, options => options.Ignore())
                      .ForMember(entity => entity.CreatedUserId, options => options.Ignore())
                      .ForMember(entity => entity.UpdatedDate, options => options.Ignore())
                      .ForMember(entity => entity.UpdatedUserId, options => options.Ignore());
            return expression;
        }

        //public static IMappingExpression<TSource, TDestination> IgnoreVersionableDestinationFields<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        //    where TDestination : IVersionable
        //{
        //    expression.ForMember(destination => destination.RowVersion, options => options.Ignore());
        //    return expression;
        //}

        public static IMappingExpression<TSource, TDestination> IgnorePersistableDestinationFields<TSource, TDestination, TDestinationId>(this IMappingExpression<TSource, TDestination> expression)
            where TDestination : IPersistable<TDestinationId>
        {
            expression.ForMember(destination => destination.Id, options => options.Ignore());
            return expression;
        }
    }
}