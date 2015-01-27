using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDeleteList<in TEntity> :ISupports
    {
        void Delete(IEnumerable<TEntity> entities);
    }
}
