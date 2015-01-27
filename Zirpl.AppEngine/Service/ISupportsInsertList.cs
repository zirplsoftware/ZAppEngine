using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsInsertList<in TEntity> :ISupports
    {
        void Insert(IEnumerable<TEntity> entities);
    }
}
