namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDelete<TEntity> :ISupports
    {
        void Delete(TEntity entity);
    }
}
