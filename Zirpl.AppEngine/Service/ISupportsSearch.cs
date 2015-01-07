using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsSearch<TEntity> : ISupports
    {
        SearchResults<TEntity> Search(ISearchCriteria searchCriteria);
    }
}
