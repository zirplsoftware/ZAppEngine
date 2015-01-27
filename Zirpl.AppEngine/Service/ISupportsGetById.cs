namespace Zirpl.AppEngine.Service
{
    public interface ISupportsGetById<out TEntity, in TId> : ISupports
    {
        TEntity Get(TId id);
    }
}
