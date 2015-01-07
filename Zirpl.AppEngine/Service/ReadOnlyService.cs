using System;
using System.Collections.Generic;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.Service
{
    public class ReadOnlyService<TEntity, TId> : AbstractedSupportsImplService<TEntity, TId>, IReadOnlyService<TEntity, TId>
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
    {
        public ReadOnlyService()
        {

        }
        public ReadOnlyService(IReadOnlyDataService<TEntity, TId> dataService)
        {
            this.DataService = dataService;
        }

        public override void Insert(IEnumerable<TEntity> entities)
        {
            throw new InvalidOperationException();
        }
        public override void Insert(TEntity entity)
        {
            throw new InvalidOperationException();
        }
        public override void Delete(IEnumerable<TEntity> entities)
        {
            throw new InvalidOperationException();
        }
        public override void Delete(ISearchCriteria searchCriteria)
        {
            throw new InvalidOperationException();
        }
        public override void Delete(TEntity entity)
        {
            throw new InvalidOperationException();
        }
        public override void DeleteById(IEnumerable<TId> ids)
        {
            throw new InvalidOperationException();
        }
        public override void DeleteById(TId id)
        {
            throw new InvalidOperationException();
        }
        public override void Update(IEnumerable<TEntity> entities)
        {
            throw new InvalidOperationException();
        }
        public override void Update(TEntity entity)
        {
            throw new InvalidOperationException();
        }
    }
}
