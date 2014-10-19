using Zirpl.AppEngine.Model.Search;
#if !SILVERLIGHT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Zirpl.AppEngine.Model;

namespace Zirpl.AppEngine.Service
{
    public class TransactionalAbstractedSupportsImplService<TEntity, TId> : AbstractedSupportsImplService<TEntity, TId> 
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
    {
        public bool DeleteRequiresNewTransaction { get; set; }
        public bool InsertRequiresNewTransaction { get; set; }
        public bool SaveRequiresNewTransaction { get; set; }
        public bool UpdateRequiresNewTransaction { get; set; }
        

        public override void Delete(TEntity entity)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.DeleteRequiresNewTransaction 
                                                    ? TransactionScopeOption.RequiresNew : 
                                                    TransactionScopeOption.Required))
            {
                base.Delete(entity);

                transaction.Complete();
            }
        }

        public override void DeleteById(TId id)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.DeleteRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.DeleteById(id);

                transaction.Complete();
            }
        }

        public override void Delete(IEnumerable<TEntity> entities)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.DeleteRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.Delete(entities);

                transaction.Complete();
            }
        }

        public override void DeleteById(IEnumerable<TId> ids)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.DeleteRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.DeleteById(ids);

                transaction.Complete();
            }
        }

        public override void Delete(ISearchCriteria searchCriteria)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.DeleteRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.Delete(searchCriteria);

                transaction.Complete();
            }
        }

        public override TEntity Get(TId id)
        {
            TEntity entity = default(TEntity);
            
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                entity = base.Get(id);

                transaction.Complete();
            }

            return entity;
        }

        public override ICollection<TEntity> GetAll()
        {
            ICollection<TEntity> entities = null;

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                entities = base.GetAll();

                transaction.Complete();
            }

            return entities;
        }

        public override void Insert(TEntity entity)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.InsertRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.Insert(entity);

                transaction.Complete();
            }
        }

        public override void Insert(IEnumerable<TEntity> entities)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.InsertRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.Insert(entities);

                transaction.Complete();
            }
        }

        public override void Save(TEntity entity)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.SaveRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.Save(entity);

                transaction.Complete();
            }
        }

        public override void Save(IEnumerable<TEntity> entities)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.SaveRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.Save(entities);

                transaction.Complete();
            }
        }

        public override void Update(TEntity entity)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.UpdateRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.Update(entity);

                transaction.Complete();
            }
        }

        public override void Update(IEnumerable<TEntity> entities)
        {
            using (TransactionScope transaction = new TransactionScope(
                                                    this.UpdateRequiresNewTransaction
                                                    ? TransactionScopeOption.RequiresNew :
                                                    TransactionScopeOption.Required))
            {
                base.Update(entities);

                transaction.Complete();
            }
        }

        public override bool Exists(TId id)
        {
            bool exists = false;
            
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                exists = base.Exists(id);

                transaction.Complete();
            }

            return exists;
        }

        public override int GetTotalCount(ISearchCriteria searchCriteria)
        {
            int totalCount = 0;
            
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                totalCount = base.GetTotalCount(searchCriteria);

                transaction.Complete();
            }

            return totalCount;
        }

        public override SearchResults<TEntity> Search(ISearchCriteria searchCriteria)
        {
            SearchResults<TEntity> result = null;
            
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                base.Search(searchCriteria);

                transaction.Complete();
            }

            return result;
        }

        public override IQueryable<TEntity> GetQueryable()
        {
            IQueryable<TEntity> result = null;
            
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                result = base.GetQueryable();

                transaction.Complete();
            }

            return result;
        }

        public override TEntity SearchUnique(ISearchCriteria searchCriteria)
        {
            TEntity result = default(TEntity);
            
            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Suppress))
            {
                result = base.SearchUnique(searchCriteria);

                transaction.Complete();
            }

            return result;
        }
    }
}
#endif