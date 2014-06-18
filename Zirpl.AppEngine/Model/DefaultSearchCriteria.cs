using System;

namespace Zirpl.AppEngine.Model
{
    public class DefaultSearchCriteria: ISearchCriteria
    {
        public DefaultSearchCriteria()
        {
            this.MaxResults = Int32.MaxValue;
        }
        public int MaxResults { get; set; }

        public int StartIndex { get; set; }
    }
}
