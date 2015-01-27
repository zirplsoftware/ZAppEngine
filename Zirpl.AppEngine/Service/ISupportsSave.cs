namespace Zirpl.AppEngine.Service
{
    public interface ISupportsSave<in TEntity> : ISupports
    {
        void Save(TEntity entity);
    }
}
