namespace Zirpl.AppEngine.Service
{
    /// <summary>
    /// Marker interface for a class that implements other ISupports interfaces
    /// </summary>
    public interface IService<TEntity, TId> :IService where TEntity : class
    {
    }
}
