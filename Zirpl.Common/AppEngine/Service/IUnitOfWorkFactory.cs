namespace Zirpl.AppEngine.Service
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
