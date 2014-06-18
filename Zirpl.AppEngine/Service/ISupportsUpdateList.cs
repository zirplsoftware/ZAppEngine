using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsUpdateList<TEntity> :ISupports
    {
        void Update(IEnumerable<TEntity> entities);
    }
}
