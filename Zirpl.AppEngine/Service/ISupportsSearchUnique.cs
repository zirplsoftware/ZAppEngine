using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsSearchUnique<out TEntity> : ISupports
    {
        TEntity SearchUnique(ISearchCriteria searchCriteria);
    }
}
