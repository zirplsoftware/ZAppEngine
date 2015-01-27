using System.Linq;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsQueryable<out TEntity> : ISupports
    {
        IQueryable<TEntity> GetQueryable();
    }
}
