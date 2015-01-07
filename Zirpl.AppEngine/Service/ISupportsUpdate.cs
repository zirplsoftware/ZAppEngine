namespace Zirpl.AppEngine.Service
{
    public interface ISupportsUpdate<TEntity> : ISupports
    {
        void Update(TEntity entity);
    }
}
