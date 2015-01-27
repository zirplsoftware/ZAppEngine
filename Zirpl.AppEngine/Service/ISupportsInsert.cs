namespace Zirpl.AppEngine.Service
{
    public interface ISupportsInsert<in TEntity> : ISupports
    {
        void Insert(TEntity entity);
    }
}
