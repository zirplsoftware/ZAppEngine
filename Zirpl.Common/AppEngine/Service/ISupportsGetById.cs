namespace Zirpl.AppEngine.Service
{
    public interface ISupportsGetById<TEntity, TId> : ISupports
    {
        TEntity Get(TId id);
    }
}
