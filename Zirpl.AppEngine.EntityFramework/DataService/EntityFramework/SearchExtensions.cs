using System;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.EntityFramework;
using Zirpl.AppEngine.Model.Search;
using Zirpl.Linq;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public static class SearchExtensions
    {
        public static IQueryable<TEntity> ApplyBoundingSearchCriteria<TEntity, TId>(this IQueryable<TEntity> query, ISearchCriteria searchCriteria)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
        {
            return query.Skip(searchCriteria.StartIndex).Take(searchCriteria.MaxResults);
        }

        public static IQueryable<TEntity> ApplyNonBoundingSearchCriteria<TEntity, TId>(this IQueryable<TEntity> query, ISearchCriteria searchCriteria)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
        {
            return ApplyNonBoundingSearchCriteria<TEntity, TId>(query, searchCriteria, null);
        }

        public static IQueryable<TEntity> ApplyNonBoundingSearchCriteria<TEntity, TId>(this IQueryable<TEntity> query, ISearchCriteria searchCriteria, ISearchCriteriaTranslator<TEntity> searchCriteriaTranslator)
            where TEntity : class, IPersistable<TId>
            where TId : IEquatable<TId>
        {
            // default sort
            query = query.OrderBy(o => o.Id);
            if (searchCriteria is LinqExpressionSearchCriteria<TEntity>)
            {
                LinqExpressionSearchCriteria<TEntity> linqExpressionSearchCriteria = (LinqExpressionSearchCriteria<TEntity>)searchCriteria;
                if (linqExpressionSearchCriteria.WhereExpression != null)
                {
                    query = query.Where(linqExpressionSearchCriteria.WhereExpression);
                }
                if (!String.IsNullOrEmpty(linqExpressionSearchCriteria.OrderByClause))
                {
                    query = query.OrderBy(linqExpressionSearchCriteria.OrderByClause);
                }
            }

            if (searchCriteriaTranslator != null)
            {
                query = searchCriteriaTranslator.ProcessSearchCriteria(query, searchCriteria);
            }

            return query;
        }
    }
}
