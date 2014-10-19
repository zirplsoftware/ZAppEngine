using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public interface ISearchCriteriaTranslator<TEntity>
    {
        IQueryable<TEntity> ProcessSearchCriteria(IQueryable<TEntity> query, ISearchCriteria searchCriteria);
    }
}
