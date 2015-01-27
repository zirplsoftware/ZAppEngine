using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Search;

namespace Zirpl.AppEngine.DataService.EntityFramework
{
    public abstract class DbContextDataServiceBase<TContext, TEntity, TId> : IDataService
        where TEntity : class, IPersistable<TId>
        where TContext : DbContext
        where TId : IEquatable<TId>
    {
        public TContext DataContext {get; set;}
        public ISearchCriteriaTranslator<TEntity> SearchCriteriaTranslator { get; set; }
        public IDbContextCudHandler DataContextCudHandler { get; set; }
        public IRetryPolicyFactory RetryPolicyFactory { get; set; }
        
        public DbContextDataServiceBase()
        {
        }

        public DbContextDataServiceBase(TContext dataContext)
        {
            this.DataContext = dataContext;
        }

        protected IDbSet<TEntity> DbSet { get { return this.DataContext.Set<TEntity>(); } }
        
        public virtual TEntity Create()
        {
            var entity = this.DbSet.Create();
            this.DataContextCudHandler.MarkInserted<TContext, TEntity, TId>(this.DataContext, entity);
            return entity;
        }

        public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            var entity = this.DbSet.Create<TDerivedEntity>();
            this.DataContextCudHandler.MarkInserted<TContext, TEntity, TId>(this.DataContext, entity);
            return entity;
        }

        public virtual void Reload(TEntity entity)
        {
            // TOTEST: test
            // TODO: add support for reloading a list

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!this.IsPersisted(entity))
            {
                throw new ArgumentException("Cannot Reload an unpersisted entity", "entity");
            }

            if (this.RetryPolicyFactory != null)
            {
                this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction(() => this.DataContext.Entry(entity).Reload());
            }
            else
            {
                this.DataContext.Entry(entity).Reload();   
            }
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!this.IsPersisted(entity))
            {
                throw new ArgumentException("Cannot Delete an unpersisted entity", "entity");
            }

            this.DoDelete(entity);
        }

        private void DoDelete(TEntity entity)
        {
            this.DataContextCudHandler.MarkDeleted<TContext, TEntity, TId>(this.DataContext, entity);
        }

        public virtual void DeleteById(TId id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot delete by nonpersisted id", "id");
            }

            this.DoDeleteById(id);
        }

        private void DoDeleteById(TId id)
        {
            var entity = this.DbSet.Find(id);
            if (entity != null)
            {
                this.DoDelete(entity);
            }
        }
           
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            var list = entities.ToList();
            if (!list.Any())
            {
                throw new ArgumentException("Cannot delete from an empty list", "entities");
            }

            foreach (var entity in list)
            {
                if (entity == null
                    || !this.IsPersisted(entity))
                {
                    throw new ArgumentException("Cannot Delete a null or unpersisted entity", "entities");
                }

                this.DoDelete(entity);
            }
        }

        public virtual void DeleteById(IEnumerable<TId> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException("ids");
            }
            var list = ids.ToList();
            if (!list.Any())
            {
                throw new ArgumentException("Cannot delete from an empty list", "ids");
            }

            foreach (var id in list)
            {
                if (id.IsNullId())
                {
                    throw new ArgumentException("Cannot delete by a nonpersisted id value", "ids");
                }

                this.DoDeleteById(id);
            }
        }

        public virtual TEntity Get(TId id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot get by unpersisted id", "id");
            }

            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<TEntity>(() => this.DbSet.Find(id))
                : this.DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var query = from o in this.DbSet
                        select o;
            query = this.ApplyDefaultSort(query);
            var returnCollection = query.ToArray();
            return returnCollection;
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (this.IsPersisted(entity))
            {
                throw new ArgumentException("Cannot insert persisted entity", "entity");
            }

            this.DoInsert(entity);
        }

        protected virtual bool IsPersisted(TEntity entity)
        {
            return entity.IsPersisted;
        }

        private void DoInsert(TEntity entity)
        {
            this.DataContextCudHandler.MarkInserted<TContext, TEntity, TId>(this.DataContext, entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            var list = entities.ToList();
            if (!list.Any())
            {
                throw new ArgumentException("Cannot Insert empty list", "entities");
            }

            foreach (var entity in list)
            {
                if (entity == null
                    || this.IsPersisted(entity))
                {
                    throw new ArgumentException("Cannot insert a null or persisted entity", "entities");
                }

                this.DoInsert(entity);
            }
        }

        public virtual void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.IsPersisted)
            {
                this.DoUpdate(entity);
            }
            else
            {
                this.DoInsert(entity);
            }
        }

        public virtual void Save(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            var list = entities.ToList();
            if (!list.Any())
            {
                throw new ArgumentException("Cannot Save empty list", "entities");
            }

            foreach (var entity in list)
            {
                if (entity == null)
                {
                    throw new ArgumentException("Cannot Save null entity", "entities");
                }

                if (entity.IsPersisted)
                {
                    this.DoUpdate(entity);
                }
                else
                {
                    this.DoInsert(entity);
                }
            }
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!this.IsPersisted(entity))
            {
                throw new ArgumentException("Cannot update nonpersisted entity", "entity");
            }

            this.DoUpdate(entity);
        }

        private void DoUpdate(TEntity entity)
        {
            this.DataContextCudHandler.MarkUpdated<TContext, TEntity, TId>(this.DataContext, entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            var list = entities.ToList();
            if (!list.Any())
            {
                throw  new ArgumentException("Cannot Update empty list", "entities");
            }

            foreach (var entity in list)
            {
                if (entity == null
                    || !this.IsPersisted(entity))
                {
                    throw  new ArgumentException("Cannot update null or nonpersisted entity", "entities");
                }

                this.DoUpdate(entity);
            }
        }

        public virtual bool Exists(TId id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot check for nonpersisted id", "id");
            }

            var exists = this.Get(id) != null;
            return exists;
        }

        public virtual int GetTotalCount(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<TEntity, TId>(searchCriteria, this.SearchCriteriaTranslator);

            return this.RetryPolicyFactory != null 
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<int>(query.Count)
                : query.Count();
        }

        public virtual SearchResults<TEntity> Search(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var searchResults = new SearchResults<TEntity> {SearchCriteria = searchCriteria};

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<TEntity, TId>(searchCriteria, this.SearchCriteriaTranslator);
            query = this.ApplyDefaultSort(query);
            searchResults.TotalCount = query.Count();
            query = query.ApplyBoundingSearchCriteria<TEntity, TId>(searchCriteria);

            searchResults.Results = this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<IList<TEntity>>(query.ToList)
                : query.ToList();
            
            return searchResults;
        }

        public virtual TEntity SearchUnique(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<TEntity, TId>(searchCriteria, this.SearchCriteriaTranslator);

            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<TEntity>(query.SingleOrDefault)
                : query.SingleOrDefault();
        }

        public virtual void Delete(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<TEntity, TId>(searchCriteria, this.SearchCriteriaTranslator);
            foreach (var entity in query.ToList())
            {
                this.DoDelete(entity);
            }
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            var result = from o in this.DbSet
                        select o;

            result = this.ApplyDefaultSort(result);
            return result;
        }

        protected virtual IQueryable<TEntity> ApplyDefaultSort(IQueryable<TEntity> query)
        {
            query = from o in query
                         orderby o.Id
                         select o;

            return query;
            //return new QueryableWrapper<TEntity>(query, null);
        }
    }
}
