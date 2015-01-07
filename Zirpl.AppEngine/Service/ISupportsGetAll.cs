using System.Collections.Generic;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsGetAll<TEntity> : ISupports
    {
        ICollection<TEntity> GetAll();
    }
}
