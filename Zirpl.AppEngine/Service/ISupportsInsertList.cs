using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsInsertList<TEntity> :ISupports
    {
        void Insert(IEnumerable<TEntity> entities);
    }
}
