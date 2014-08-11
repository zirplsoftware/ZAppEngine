using System;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Testing
{
    public interface IEntityLayerTestsStrategy<TEntity, TId, TEntityWrapper>
        where TEntity : class, IPersistable<TId>
        where TId : struct, IEquatable<TId>
        where TEntityWrapper : EntityWrapper<TEntity>, new()
    {
        void SetUpWrapper(TEntityWrapper wrapper);
        void CreateEntity(TEntityWrapper wrapper);
        void OnAssertCommonPersistedEntityExpectations(TEntityWrapper entityWrapper,
                                                        TEntity entity,
                                                       TEntity entityFromDb);
        void OnAssertExpectationsAfterInsert(TEntityWrapper entityWrapper, TEntity entity,
                                             TEntity entityFromDb);
        void OnAssertExpectationsAfterUpdate(TEntityWrapper entityWrapper, TEntity entity,
                                             TEntity entityFromDb);

        void OnAssertExpectationsAfterDelete(TEntityWrapper entityWrapper);

        void ChangePropertyValues(TEntityWrapper entityWrapper, TEntity entity);

        void ChangePropertyValuesToFailValidation(TEntityWrapper entityWrapper, TEntity entity);
        TId IncrementId(TId id);
    }
}
