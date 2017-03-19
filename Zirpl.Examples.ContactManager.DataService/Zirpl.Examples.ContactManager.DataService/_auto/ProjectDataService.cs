using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Zirpl.AppEngine;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Search;

namespace Zirpl.Examples.ContactManager.DataService
{
    public partial class ProjectDataService : Zirpl.Examples.ContactManager.DataService.IProjectDataService
    {
        public Zirpl.Examples.ContactManager.DataService.AppDataContext DataContext { get; set; }
        public ISearchCriteriaTranslator<Zirpl.Examples.ContactManager.Model.Project> SearchCriteriaTranslator { get; set; }
        public IDbContextCudHandler DataContextCudHandler { get; set; }
        public IRetryPolicyFactory RetryPolicyFactory { get; set; }

        public ProjectDataService()
        {
        }

        public ProjectDataService(Zirpl.Examples.ContactManager.DataService.AppDataContext dataContext)
        {
            this.DataContext = dataContext;
        }

        protected IDbSet<Zirpl.Examples.ContactManager.Model.Project> DbSet { get { return this.DataContext.Projects; } }

        public virtual void Delete(Zirpl.Examples.ContactManager.Model.Project entity)
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

        private void DoDelete(Zirpl.Examples.ContactManager.Model.Project entity)
        {
            this.DataContextCudHandler.MarkDeleted<Zirpl.Examples.ContactManager.DataService.AppDataContext, Zirpl.Examples.ContactManager.Model.Project, long>(this.DataContext, entity);
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

        public virtual void Delete(IEnumerable<Zirpl.Examples.ContactManager.Model.Project> entities)
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

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Project, long>(searchCriteria, this.SearchCriteriaTranslator);
            foreach (var entity in query.ToList())
            {
                this.DoDelete(entity);
            }
        }


        public virtual void Reload(Zirpl.Examples.ContactManager.Model.Project entity)
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

        public virtual Zirpl.Examples.ContactManager.Model.Project Get(long id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot get by unpersisted id", "id");
            }

            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<Zirpl.Examples.ContactManager.Model.Project>(() => this.DbSet.Find(id))
                : this.DbSet.Find(id);
        }

        public virtual IEnumerable<Zirpl.Examples.ContactManager.Model.Project> GetAll()
        {
            var query = from o in this.DbSet
                        select o;
            query = this.ApplyDefaultSort(query);
            var returnCollection = query.ToArray();
            return returnCollection;
        }


        protected virtual bool IsPersisted(Zirpl.Examples.ContactManager.Model.Project entity)
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

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Project, long>(searchCriteria, this.SearchCriteriaTranslator);

            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<int>(query.Count)
                : query.Count();
        }

        public virtual SearchResults<Zirpl.Examples.ContactManager.Model.Project> Search(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var searchResults = new SearchResults<Zirpl.Examples.ContactManager.Model.Project> { SearchCriteria = searchCriteria };

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Project, long>(searchCriteria, this.SearchCriteriaTranslator);
            query = this.ApplyDefaultSort(query);
            searchResults.TotalCount = query.Count();
            query = query.ApplyBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Project, long>(searchCriteria);

            searchResults.Results = this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<IList<Zirpl.Examples.ContactManager.Model.Project>>(query.ToList)
                : query.ToList();

            return searchResults;
        }

        public virtual Zirpl.Examples.ContactManager.Model.Project SearchUnique(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var query = this.DbSet.ApplyNonBoundingSearchCriteria<Zirpl.Examples.ContactManager.Model.Project, long>(searchCriteria, this.SearchCriteriaTranslator);

            return this.RetryPolicyFactory != null
                ? this.RetryPolicyFactory.CreateRetryPolicy().ExecuteAction<Zirpl.Examples.ContactManager.Model.Project>(query.SingleOrDefault)
                : query.SingleOrDefault();
        }

        public virtual IQueryable<Zirpl.Examples.ContactManager.Model.Project> GetQueryable()
        {
            var result = from o in this.DbSet
                         select o;

            result = this.ApplyDefaultSort(result);
            return result;
        }

        protected virtual IQueryable<Zirpl.Examples.ContactManager.Model.Project> ApplyDefaultSort(IQueryable<Zirpl.Examples.ContactManager.Model.Project> query)
        {
            query = from o in query
                    orderby o.Id
                    select o;

            return query;
            //return new QueryableWrapper<Zirpl.Examples.ContactManager.Model.Project>(query, null);
        }
    }
}
