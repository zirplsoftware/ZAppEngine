using System;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Validation;

namespace Zirpl.AppEngine.Service
{
    public class AbstractedSupportsImplService<TEntity, TId> : IService, ISupports 
        where TEntity : class, IPersistable<TId>
        where TId : IEquatable<TId>
    {
        public IDataService<TEntity, TId> DataService { get; set; }
        public IValidationHelper ValidationHelper { get; set; }

        private String EntityType
        {
            get { return typeof (TEntity).Name; }
        }

        public virtual TEntity Create()
        {
            var dataService = this.DataService as ISupportsCreate<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Create");
            this.OnPreCreate();
            var entity = dataService.Create();
            this.GetLog().Debug("Post-Create");
            this.OnPostCreate(entity);

            return entity;
        }

        protected virtual void OnPreCreate()
        {
            
        }

        protected virtual void OnPostCreate(TEntity entity)
        {
            
        }

        public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            var dataService = this.DataService as ISupportsCreate<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-CreateDerived");
            this.OnPreCreate();
            var entity = dataService.Create<TDerivedEntity>();
            this.GetLog().Debug("Post-CreateDerived");
            this.OnPostCreate(entity);

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
            if (!entity.IsPersisted)
            {
                throw new ArgumentException("Cannot Reload an unpersisted entity", "entity");
            }

            var dataService = this.DataService as ISupportsReload<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Reload");
            this.OnPreReload (entity);
            dataService.Reload(entity);
            this.GetLog().Debug("Post-Reload");
            this.OnPostReload(entity);
        }

        protected virtual void OnPreReload(TEntity entity)
        {
        }

        protected virtual void OnPostReload(TEntity entity)
        {
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!entity.IsPersisted)
            {
                throw new ArgumentException("Cannot Delete an unpersisted entity", "entity");
            }

            var dataService = this.DataService as ISupportsDelete<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Delete");
            this.OnPreDelete(entity);
            dataService.Delete(entity);
            this.GetLog().Debug("Post-Delete");
            this.OnPostDelete(entity);
        }

        protected virtual void OnPreDelete(TEntity entity)
        {
            
        }
        protected virtual void OnPostDelete(TEntity entity)
        {

        }

        public virtual void DeleteById(TId id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot delete by nonpersisted id", "id");
            }

            var dataService = this.DataService as ISupportsDeleteById<TId>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-DeleteById");
            this.OnPreDeleteById(id);
            dataService.DeleteById(id);
            this.GetLog().Debug("Post-DeleteById");
            this.OnPostDeleteById(id);
        }

        protected virtual void OnPreDeleteById(TId id)
        {

        }
        protected virtual void OnPostDeleteById(TId id)
        {

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

            var dataService = this.DataService as ISupportsDeleteList<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-DeleteList");
            foreach (var entity in list)
            {
                if (entity == null
                    || !entity.IsPersisted)
                {
                    throw new ArgumentException("Cannot Delete a null or unpersisted entity", "entities");
                }

                this.OnPreDelete(entity);
            }
            dataService.Delete(list);
            this.GetLog().Debug("Post-DeleteList");
            list.ForEach(this.OnPostDelete);
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

            var dataService = this.DataService as ISupportsDeleteListByIds<TId>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-DeleteListByIds");
            foreach (var id in list)
            {
                if (id.IsNullId())
                {
                    throw new ArgumentException("Cannot delete by a nonpersisted id value", "ids");
                }

                this.OnPreDeleteById(id);
            }
            dataService.DeleteById(list);
            this.GetLog().Debug("Post-DeleteListByIds");
            list.ForEach(this.OnPostDeleteById);
        }

        public virtual void Delete(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var dataService = this.DataService as ISupportsDeleteSearch;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-DeleteSearch");
            this.OnPreDelete(searchCriteria);
            dataService.Delete(searchCriteria);
            this.GetLog().Debug("Post-DeleteSearch");
            this.OnPostDelete(searchCriteria);
        }

        protected virtual void OnPreDelete(ISearchCriteria searchCriteria)
        {

        }
        protected virtual void OnPostDelete(ISearchCriteria searchCriteria)
        {

        }

        public virtual TEntity Get(TId id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot get by unpersisted id", "id");
            }

            var dataService = this.DataService as ISupportsGetById<TEntity, TId>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Get");
            this.OnPreGet(id);
            var entity = dataService.Get(id);
            this.GetLog().Debug("Post-Get");
            this.OnPostGet(id, entity);

            return entity;
        }

        protected virtual void OnPreGet(TId id)
        {
            
        }

        protected virtual void OnPostGet(TId id, TEntity entity)
        {
            
        }

        public virtual ICollection<TEntity> GetAll()
        {
            var dataService = this.DataService as ISupportsGetAll<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-GetAll");
            this.OnPreGetAll();
            var entities = dataService.GetAll();
            this.GetLog().Debug("Post-GetAll");
            this.OnPostGetAll(entities);

            return entities;
        }

        protected virtual void OnPreGetAll()
        {

        }

        protected virtual void OnPostGetAll(ICollection<TEntity> entities)
        {

        }

        public virtual void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (entity.IsPersisted)
            {
                throw new ArgumentException("Cannot insert persisted entity", "entity");
            }

            var dataService = this.DataService as ISupportsInsert<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Insert");
            this.OnPreInsertPreValidate(entity);
            this.Validate(ServiceAction.Insert, entity);
            this.OnPreInsertPostValidate(entity);
            dataService.Insert(entity);
            this.GetLog().Debug("Post-Insert");
            this.OnPostInsert(entity);
        }

        protected virtual void Validate(ServiceAction serviceAction, TEntity entity)
        {
            
        }

        protected virtual void OnPreInsertPreValidate(TEntity entity)
        {

        }

        protected virtual void OnPreInsertPostValidate(TEntity entity)
        {

        }

        protected virtual void OnPostInsert(TEntity entity)
        {

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

            var dataService = this.DataService as ISupportsInsertList<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-AddList");

            foreach (var entity in list)
            {
                if (entity == null
                    || entity.IsPersisted)
                {
                    throw new ArgumentException("Cannot insert a null or persisted entity", "entities");
                }

                this.OnPreInsertPreValidate(entity);
            }
            list.ForEach(entity => this.Validate(ServiceAction.Insert, entity));
            list.ForEach(this.OnPreInsertPostValidate);
            dataService.Insert(list);
            this.GetLog().Debug("Post-AddList");
            list.ForEach(this.OnPostInsert);
        }

        public virtual void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var dataService = this.DataService as ISupportsSave<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Save");

            if (entity.IsPersisted)
            {
                this.OnPreUpdatePreValidate(entity);
                this.Validate(ServiceAction.Update, entity);
                this.OnPreUpdatePostValidate(entity);
            }
            else
            {
                this.OnPreInsertPreValidate(entity);
                this.Validate(ServiceAction.Insert, entity);
                this.OnPreInsertPostValidate(entity);
            }

            dataService.Save(entity);

            this.GetLog().Debug("Post-Save");

            if (entity.IsPersisted)
            {
                this.OnPostUpdate(entity);
            }
            else
            {
                this.OnPostInsert(entity);
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

            var dataService = this.DataService as ISupportsSaveList<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }

            this.GetLog().Debug("Pre-Save");

            var serviceActions = new Dictionary<TEntity, ServiceAction>();
            foreach (var t in list)
            {
                if (t == null)
                {
                    throw new ArgumentException("Cannot Save null entity", "entities");
                }
                serviceActions.Add(t, t.IsPersisted ? ServiceAction.Update : ServiceAction.Insert);
            }

            foreach (var t in list)
            {
                var action = serviceActions[t];
                if (action == ServiceAction.Insert)
                {
                    this.OnPreInsertPreValidate(t);
                    this.Validate(ServiceAction.Insert, t);
                    this.OnPreInsertPostValidate(t);
                }
                else
                {
                    this.OnPreUpdatePreValidate(t);
                    this.Validate(ServiceAction.Update, t);
                    this.OnPreUpdatePostValidate(t);
                }
            }

            dataService.Save(list);

            this.GetLog().Debug("Post-Save");

            foreach (var t in list)
            {
                var action = serviceActions[t];
                if (action == ServiceAction.Insert)
                {
                    this.OnPostInsert(t);
                }
                else
                {
                    this.OnPostUpdate(t);
                }
            }
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (!entity.IsPersisted)
            {
                throw new ArgumentException("Cannot update nonpersisted entity", "entity");
            }

            var dataService = this.DataService as ISupportsUpdate<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Update");
            this.OnPreUpdatePreValidate(entity);
            this.Validate(ServiceAction.Update, entity);
            this.OnPreUpdatePostValidate(entity);
            dataService.Update(entity);
            this.GetLog().Debug("Post-Update");
            this.OnPostUpdate(entity);
        }

        protected virtual void OnPreUpdatePreValidate(TEntity entity)
        {

        }

        protected virtual void OnPreUpdatePostValidate(TEntity entity)
        {

        }

        protected virtual void OnPostUpdate(TEntity entity)
        {

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
                throw new ArgumentException("Cannot Update empty list", "entities");
            }

            var dataService = this.DataService as ISupportsUpdateList<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Update");
            foreach (var entity in list)
            {
                if (entity == null
                    || !entity.IsPersisted)
                {
                    throw new ArgumentException("Cannot update null or nonpersisted entity", "entities");
                }

                this.OnPreUpdatePreValidate(entity);
            }
            list.ForEach(entity => this.Validate(ServiceAction.Update, entity));
            list.ForEach(OnPreUpdatePostValidate);
            dataService.Update(list);
            this.GetLog().Debug("Post-Update");
            list.ForEach(OnPostUpdate);
        }

        public virtual bool Exists(TId id)
        {
            if (id.IsNullId())
            {
                throw new ArgumentException("Cannot check for nonpersisted id", "id");
            }

            var dataService = this.DataService as ISupportsExists<TId>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Exists");
            this.OnPreExists(id);
            var exists = dataService.Exists(id);
            this.GetLog().Debug("Post-Exists");
            this.OnPostExists(id, exists);

            return exists;
        }

        protected virtual void OnPreExists(TId id)
        {

        }

        protected virtual void OnPostExists(TId id, bool exists)
        {

        }

        public virtual int GetTotalCount(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var dataService = this.DataService as ISupportsGetTotalCount;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-GetTotalCount");
            this.OnPreGetTotalCount(searchCriteria);
            var totalCount = dataService.GetTotalCount(searchCriteria);
            this.GetLog().Debug("Post-GetTotalCount");
            this.OnPostGetTotalCount(searchCriteria, totalCount);

            return totalCount;
        }

        protected virtual void OnPreGetTotalCount(ISearchCriteria searchCriteria)
        {

        }

        protected virtual void OnPostGetTotalCount(ISearchCriteria searchCriteria, int count)
        {

        }

        public virtual SearchResults<TEntity> Search(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var dataService = this.DataService as ISupportsSearch<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Search");
            this.OnPreSearch(searchCriteria);
            var result = dataService.Search(searchCriteria);
            this.GetLog().Debug("Post-Search");
            this.OnPostSearch(searchCriteria, result);

            return result;
        }

        protected virtual void OnPreSearch(ISearchCriteria searchCriteria)
        {

        }

        protected virtual void OnPostSearch(ISearchCriteria searchCriteria, SearchResults<TEntity> result)
        {

        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            var dataService = this.DataService as ISupportsQueryable<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-Search");
            this.OnPreGetQueryable();
            var result = dataService.GetQueryable();
            this.GetLog().Debug("Post-Search");
            this.OnPostGetQueryable(result);

            return result;
        }

        protected virtual void OnPreGetQueryable()
        {

        }

        protected virtual void OnPostGetQueryable(IQueryable<TEntity> queryable)
        {

        }

        public virtual TEntity SearchUnique(ISearchCriteria searchCriteria)
        {
            if (searchCriteria == null)
            {
                throw new ArgumentNullException("searchCriteria");
            }

            var dataService = this.DataService as ISupportsSearchUnique<TEntity>;
            if (dataService == null)
            {
                throw new InvalidOperationException();
            }
            this.GetLog().Debug("Pre-SearchUnique");
            this.OnPreSearchUnique(searchCriteria);
            var result = dataService.SearchUnique(searchCriteria);
            this.GetLog().Debug("Post-SearchUnique");
            this.OnPostSearchUnique(searchCriteria, result);

            return result;
        }

        protected virtual void OnPreSearchUnique(ISearchCriteria searchCriteria)
        {

        }

        protected virtual void OnPostSearchUnique(ISearchCriteria searchCriteria, TEntity entity)
        {

        }
    }
}
