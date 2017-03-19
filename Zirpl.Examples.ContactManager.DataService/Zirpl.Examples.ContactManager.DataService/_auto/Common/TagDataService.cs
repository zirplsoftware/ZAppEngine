using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Zirpl.AppEngine;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Search;

namespace Zirpl.Examples.ContactManager.DataService.Common
{
    public partial class TagDataService : Zirpl.Examples.ContactManager.DataService.Common.ITagDataService
    {
        public Zirpl.Examples.ContactManager.DataService.AppDataContext DataContext { get; set; }
        public ISearchCriteriaTranslator<Zirpl.Examples.ContactManager.Model.Common.Tag> SearchCriteriaTranslator { get; set; }
        public IDbContextCudHandler DataContextCudHandler { get; set; }
        public IRetryPolicyFactory RetryPolicyFactory { get; set; }

        public TagDataService()
        {
        }

        public TagDataService(Zirpl.Examples.ContactManager.DataService.AppDataContext dataContext)
        {
            this.DataContext = dataContext;
        }

        protected IDbSet<Zirpl.Examples.ContactManager.Model.Common.Tag> DbSet { get { return this.DataContext.Tags; } }

        public virtual Zirpl.Examples.ContactManager.Model.Common.Tag Create()
        {
            var entity = this.DbSet.Create();
            this.DataContextCudHandler.MarkInserted<Zirpl.Examples.ContactManager.DataService.AppDataContext, Zirpl.Examples.ContactManager.Model.Common.Tag, long>(this.DataContext, entity);
            return entity;
        }

        public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : Zirpl.Examples.ContactManager.Model.Common.Tag
        {
            var entity = this.DbSet.Create<TDerivedEntity>();
            this.DataContextCudHandler.MarkInserted<Zirpl.Examples.ContactManager.DataService.AppDataContext, Zirpl.Examples.ContactManager.Model.Common.Tag, long>(this.DataContext, entity);
            return entity;
        }

        public virtual void Insert(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
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
        private void DoInsert(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
        {
            this.DataContextCudHandler.MarkInserted<Zirpl.Examples.ContactManager.DataService.AppDataContext, Zirpl.Examples.ContactManager.Model.Common.Tag, long>(this.DataContext, entity);
        }

        public virtual void Insert(IEnumerable<Zirpl.Examples.ContactManager.Model.Common.Tag> entities)
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
        public virtual void Delete(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
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

        private void DoDelete(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
        {
            this.DataContextCudHandler.MarkDeleted<Zirpl.Examples.ContactManager.DataService.AppDataContext, Zirpl.Examples.ContactManager.Model.Common.Tag, long>(this.DataContext, entity);
        }

        public virtual void DeleteById(long id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot delete by nonpersisted id", "id");
            }

            this.DoDeleteById(id);
        }

        private void DoDeleteById(long id)
        {
            var entity = this.DbSet.Find(id);
            if (entity != null)
            {
                this.DoDelete(entity);
            }
        }

        public virtual void Delete(IEnumerable<Zirpl.Examples.ContactManager.Model.Common.Tag> entities)
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

        public virtual void DeleteById(IEnumerable<long> ids)
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

        public virtual void Delete(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Common.Tag, long>(searchCriteria, this.SearchCriteriaTranslator);
            foreach (var entity in query.ToList())
            {
                this.DoDelete(entity);
            }
        }

        public virtual void Save(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
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

        public virtual void Save(IEnumerable<Zirpl.Examples.ContactManager.Model.Common.Tag> entities)
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

        public virtual void Update(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
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

        private void DoUpdate(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
        {
            this.DataContextCudHandler.MarkUpdated<Zirpl.Examples.ContactManager.DataService.AppDataContext, Zirpl.Examples.ContactManager.Model.Common.Tag, long>(this.DataContext, entity);
        }

        public virtual void Update(IEnumerable<Zirpl.Examples.ContactManager.Model.Common.Tag> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            var list = entities.ToList();
            if (!list.Any())
            {
                throw new ArgumentException("Cannot Update empty list", "entities");
            }

            foreach (var entity in list)
            {
                if (entity == null
                    || !this.IsPersisted(entity))
                {
                    throw new ArgumentException("Cannot update null or nonpersisted entity", "entities");
                }

                this.DoUpdate(entity);
            }
        }

        public virtual void Reload(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
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

        public virtual Zirpl.Examples.ContactManager.Model.Common.Tag Get(long id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot get by unpersisted id", "id");
            }

            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<Zirpl.Examples.ContactManager.Model.Common.Tag>(() => this.DbSet.Find(id))
                : this.DbSet.Find(id);
        }

        public virtual IEnumerable<Zirpl.Examples.ContactManager.Model.Common.Tag> GetAll()
        {
            var query = from o in this.DbSet
                        select o;
            query = this.ApplyDefaultSort(query);
            var returnCollection = query.ToArray();
            return returnCollection;
        }


        protected virtual bool IsPersisted(Zirpl.Examples.ContactManager.Model.Common.Tag entity)
        {
            return entity.IsPersisted;
        }

        public virtual bool Exists(long id)
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

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Common.Tag, long>(searchCriteria, this.SearchCriteriaTranslator);

            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<int>(query.Count)
                : query.Count();
        }

        public virtual SearchResults<Zirpl.Examples.ContactManager.Model.Common.Tag> Search(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var searchResults = new SearchResults<Zirpl.Examples.ContactManager.Model.Common.Tag> { SearchCriteria = searchCriteria };

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Common.Tag, long>(searchCriteria, this.SearchCriteriaTranslator);
            query = this.ApplyDefaultSort(query);
            searchResults.TotalCount = query.Count();
            query = query.ApplyBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Common.Tag, long>(searchCriteria);

            searchResults.Results = this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<IList<Zirpl.Examples.ContactManager.Model.Common.Tag>>(query.ToList)
                : query.ToList();

            return searchResults;
        }

        public virtual Zirpl.Examples.ContactManager.Model.Common.Tag SearchUnique(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Common.Tag, long>(searchCriteria, this.SearchCriteriaTranslator);

            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<Zirpl.Examples.ContactManager.Model.Common.Tag>(query.SingleOrDefault)
                : query.SingleOrDefault();
        }

        public virtual IQueryable<Zirpl.Examples.ContactManager.Model.Common.Tag> GetQueryable()
        {
            var result = from o in this.DbSet
                         select o;

            result = this.ApplyDefaultSort(result);
            return result;
        }

        protected virtual IQueryable<Zirpl.Examples.ContactManager.Model.Common.Tag> ApplyDefaultSort(IQueryable<Zirpl.Examples.ContactManager.Model.Common.Tag> query)
        {
            query = from o in query
                    orderby o.Id
                    select o;

            return query;
            //return new QueryableWrapper<Zirpl.Examples.ContactManager.Model.Common.Tag>(query, null);
        }
    }
}
