
namespace Zirpl.AppEngine.Model.Search
{
    public interface ISearchCriteria
    {
        int MaxResults { get; set; }
        int StartIndex { get; set; }
    }
}
