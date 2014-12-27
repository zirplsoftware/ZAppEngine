namespace Zirpl.AppEngine.Service
{
    public interface ISupportsReload<TEntity> : ISupports
    {
        void Reload(TEntity entity);
    }
}
