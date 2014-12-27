using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsGetTotalCount : ISupports
    {
        int GetTotalCount(ISearchCriteria searchCriteria);
    }
}
