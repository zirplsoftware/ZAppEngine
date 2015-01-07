using System.Linq;

namespace Zirpl.AppEngine.Service
{
    public interface ISupportsQueryable<TEntity> : ISupports
    {
        IQueryable<TEntity> GetQueryable();
    }
}
