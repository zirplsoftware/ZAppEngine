using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDeleteList<TEntity> :ISupports
    {
        void Delete(IEnumerable<TEntity> entities);
    }
}
