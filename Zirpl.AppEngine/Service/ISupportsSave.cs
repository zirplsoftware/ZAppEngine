namespace Zirpl.AppEngine.Service
{
    public interface ISupportsSave<TEntity> : ISupports
    {
        void Save(TEntity entity);
    }
}
