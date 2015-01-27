namespace Zirpl.AppEngine.Service
{
    public interface ISupportsReload<in TEntity> : ISupports
    {
        void Reload(TEntity entity);
    }
}
