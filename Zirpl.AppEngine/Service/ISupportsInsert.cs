namespace Zirpl.AppEngine.Service
{
    public interface ISupportsInsert<TEntity> : ISupports
    {
        void Insert(TEntity entity);
    }
}
