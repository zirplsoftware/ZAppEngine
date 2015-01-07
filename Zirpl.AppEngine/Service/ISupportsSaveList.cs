using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsSaveList<TEntity> :ISupports
    {
        void Save(IEnumerable<TEntity> entities);
    }
}
