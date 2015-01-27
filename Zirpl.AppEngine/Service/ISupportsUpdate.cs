namespace Zirpl.AppEngine.Service
{
    public interface ISupportsUpdate<in TEntity> : ISupports
    {
        void Update(TEntity entity);
    }
}
