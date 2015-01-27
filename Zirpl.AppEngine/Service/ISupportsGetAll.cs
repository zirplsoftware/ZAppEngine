using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsGetAll<out TEntity> : ISupports
    {
        IEnumerable<TEntity> GetAll();
    }
}
