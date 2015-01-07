namespace Zirpl.AppEngine.Service
{
    public interface ISupportsCreate<TEntity> :ISupports where TEntity :class
    {
        TEntity Create();
        TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;
    }
}
