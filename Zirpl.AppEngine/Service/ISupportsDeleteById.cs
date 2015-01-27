namespace Zirpl.AppEngine.Service
{
    public interface ISupportsDeleteById<in TId> : ISupports
    {
        void DeleteById(TId id);
    }
}
