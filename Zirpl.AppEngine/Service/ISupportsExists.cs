namespace Zirpl.AppEngine.Service
{
    public interface ISupportsExists<in TId> : ISupports
    {
        bool Exists(TId id);
    }
}
