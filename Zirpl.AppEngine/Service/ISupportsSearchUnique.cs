using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsSearchUnique<TEntity> : ISupports
    {
        TEntity SearchUnique(ISearchCriteria searchCriteria);
    }
}
