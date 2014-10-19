using System.Collections.Generic;

namespace Zirpl.AppEngine.Model.Search
{
    public class SearchResults<TEntity>
    {
        public ISearchCriteria SearchCriteria { get; set; }
        public int TotalCount { get; set; }
        public IList<TEntity> Results { get; set; }
    }
}
