namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDelete<in TEntity> :ISupports
    {
        void Delete(TEntity entity);
    }
}
