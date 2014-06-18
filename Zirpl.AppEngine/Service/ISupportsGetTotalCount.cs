using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsGetTotalCount : ISupports
    {
        int GetTotalCount(ISearchCriteria searchCriteria);
    }
}
