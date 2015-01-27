using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsUpdateList<in TEntity> :ISupports
    {
        void Update(IEnumerable<TEntity> entities);
    }
}
