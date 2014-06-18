
namespace Zirpl.AppEngine.Model
{
    public interface ISearchCriteria
    {
        int MaxResults { get; set; }
        int StartIndex { get; set; }
    }
}
