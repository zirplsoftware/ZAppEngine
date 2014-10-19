using System;
using System.Linq.Expressions;
using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.Model.EntityFramework
{
    public class LinqExpressionSearchCriteria<TEntity> : DefaultSearchCriteria where TEntity : IPersistable
    {
        public LinqExpressionSearchCriteria()
        {
        }
        public LinqExpressionSearchCriteria(Expression<Func<TEntity, bool>> whereExpression)
        {
            this.WhereExpression = whereExpression;
        }

        public Expression<Func<TEntity, bool>> WhereExpression { get; set; }
        public String OrderByClause { get; set; }
    }
}
