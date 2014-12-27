namespace Zirpl.AppEngine.DataService
{
    public interface IDataService<TEntity, TId> : IDataService where TEntity : class
    {
    }
    public interface IDataService
    {
    }
}
