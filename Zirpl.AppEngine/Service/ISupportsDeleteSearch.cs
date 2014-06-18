using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDeleteSearch : ISupports
    {
        void Delete(ISearchCriteria searchCriteria);
    }
}
