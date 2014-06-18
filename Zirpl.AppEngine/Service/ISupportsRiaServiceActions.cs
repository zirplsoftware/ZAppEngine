namespace Zirpl.AppEngine.Service
{
    public interface ISupportsRiaServiceActions<TEntity> : 
        ISupportsDelete<TEntity>,
        ISupportsQueryable<TEntity>,
        ISupportsInsert<TEntity>,
        ISupportsUpdate<TEntity>
    {
    }
}
