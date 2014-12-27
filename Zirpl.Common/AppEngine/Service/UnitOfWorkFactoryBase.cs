namespace Zirpl.AppEngine.Service
{
    public abstract class UnitOfWorkFactoryBase :IUnitOfWorkFactory
    {
        public abstract IUnitOfWork Create();
    }
}
