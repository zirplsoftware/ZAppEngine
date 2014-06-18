namespace Zirpl.AppEngine.Service
{
    public interface IServiceFactory
    {
        ISupportsRiaServiceActions<TEntity> GetSupportsRiaServiceActions<TEntity>();
    }
}
