namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDeleteById<TId> : ISupports
    {
        void DeleteById(TId id);
    }
}
