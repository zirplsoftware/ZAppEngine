namespace Zirpl.AppEngine.Service
{
    public interface ISupportsExists<TId> : ISupports
    {
        bool Exists(TId id);
    }
}
