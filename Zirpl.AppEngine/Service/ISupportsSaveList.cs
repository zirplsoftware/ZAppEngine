using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsSaveList<in TEntity> :ISupports
    {
        void Save(IEnumerable<TEntity> entities);
    }
}
