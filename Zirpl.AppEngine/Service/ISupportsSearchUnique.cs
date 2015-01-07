using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsSearchUnique<TEntity> : ISupports
    {
        TEntity SearchUnique(ISearchCriteria searchCriteria);
    }
}
