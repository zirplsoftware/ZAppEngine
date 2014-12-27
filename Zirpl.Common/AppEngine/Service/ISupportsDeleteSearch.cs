using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDeleteSearch : ISupports
    {
        void Delete(ISearchCriteria searchCriteria);
    }
}
