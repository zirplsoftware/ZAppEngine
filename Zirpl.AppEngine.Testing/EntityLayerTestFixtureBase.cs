using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.Ioc;
using Zirpl.AppEngine.Logging;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.EntityFramework;
using Zirpl.AppEngine.Model.Search;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Web.Mvc.Ioc.Autofac;
using Zirpl.Logging;

namespace Zirpl.AppEngine.Testing
{
    [TestFixture]
    [MockHttpContext]
    [SetAspNetMvcDependencyResolver]
    [InitializeMappingBootstrapper]
    public abstract class EntityLayerTestFixtureBase<TEntity, TId, TEntityWrapper, TStrategy>
        where TEntity : class, IPersistable<TId>
        where TId : struct, IEquatable<TId>
        where TEntityWrapper : EntityWrapper<TEntity>, new()
        where TStrategy : IEntityLayerTestsStrategy<TEntity, TId, TEntityWrapper>, new()
    {
        public IDependencyResolver DependencyResolver { get { return IocUtils.DependencyResolver; } }
        public IList<IPersistable> EntitiesToDelete { get; private set; }
        public ITransactionalUnitOfWorkFactory UnitOfWorkFactory
        {
            get
            {
                return IocUtils.DependencyResolver.Resolve<ITransactionalUnitOfWorkFactory>();
            }
        }

        protected TStrategy Strategy { get; set; }
        protected TEntityWrapper Wrapper1 { get; private set; }
        protected TEntityWrapper Wrapper2 { get; private set; }

        protected abstract TSupports GetLayer<TSupports>() where TSupports : class, ISupports;
        protected abstract DbContextBase CreateNewDbContext();

        /// <summary>
        /// Prepopulate the wrapper with any needed relationship properties- IF any
        /// </summary>
        /// <param name="wrapper"></param>
        protected virtual void SetUpWrapper(TEntityWrapper wrapper)
        {
            this.Strategy.SetUpWrapper(wrapper);
        }
        protected virtual void CreateEntity(TEntityWrapper wrapper)
        {
            this.Strategy.CreateEntity(wrapper);
        }
        protected virtual void OnAssertCommonPersistedEntityExpectations(TEntityWrapper wrapper, TEntity entity, TEntity entityFromDb)
        {
            this.Strategy.OnAssertCommonPersistedEntityExpectations(wrapper, entity, entityFromDb);
        }
        protected virtual void OnAssertExpectationsAfterInsert(TEntityWrapper wrapper, TEntity entity, TEntity entityFromDb)
        {
            this.Strategy.OnAssertExpectationsAfterInsert(wrapper, entity, entityFromDb);
        }
        protected virtual void OnAssertExpectationsAfterUpdate(TEntityWrapper wrapper, TEntity entity, TEntity entityFromDb)
        {
            this.Strategy.OnAssertExpectationsAfterUpdate(wrapper, entity, entityFromDb);
        }
        protected virtual void OnAssertExpectationsAfterDelete(TEntityWrapper wrapper)
        {
            this.Strategy.OnAssertExpectationsAfterDelete(wrapper);
        }
        protected virtual void ChangePropertyValues(TEntityWrapper wrapper, TEntity entity)
        {
            this.Strategy.ChangePropertyValues(wrapper, entity);
        }
        protected virtual void ChangePropertyValuesToFailValidation(TEntityWrapper wrapper, TEntity entity)
        {
            this.Strategy.ChangePropertyValuesToFailValidation(wrapper, entity);
        }
        protected virtual TId IncrementId(TId id)
        {
            return this.Strategy.IncrementId(id);
        }



        [SetUp]
        public virtual void SetUp()
        {
            this.EntitiesToDelete = new List<IPersistable>();
            this.Strategy = new TStrategy();

            this.Wrapper1 = new TEntityWrapper();
            this.Wrapper2 = new TEntityWrapper();

            this.SetUpWrapper(Wrapper1);
            this.SetUpWrapper(Wrapper2);
        }


        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                using (var uof = this.UnitOfWorkFactory.CreateRequired())
                {
                    foreach (IPersistable entity in this.EntitiesToDelete)
                    {
                        var entity1 = entity;
                        if (entity1.IsPersisted
                            && this.DependencyResolver.Resolve<DbContextBase>().Set(entity1.GetType()).Find(entity1.GetId()) != null)
                        {
                            this.DependencyResolver.Resolve<DbContextBase>().Set(entity1.GetType()).Remove(entity1);
                        }
                        else if (!entity1.IsPersisted)
                        {
                            this.DependencyResolver.Resolve<DbContextBase>().Entry(entity1).State = EntityState.Detached;
                        }
                    }
                    uof.Commit();
                }
            }
            catch (Exception ex)
            {
                this.GetLog().Error(ex, "Error on TearDown");
            }
        }


        protected virtual Entities<TEntityWrapper> CreateEntities(int quantity, bool persisted)
        {
            if (quantity < 1 || quantity > 2)
            {
                throw new ArgumentOutOfRangeException("quantity");
            }

            var entities = new Entities<TEntityWrapper>();

            if (this.Wrapper1.IsUsed
                && this.Wrapper2.IsUsed)
            {
                throw new Exception("Cannot use more than 2 wrappers in a single test");
            }
            var wrapperToUse = this.Wrapper1.IsUsed ? this.Wrapper2 : this.Wrapper1;
            entities.Wrapper1 = wrapperToUse;

            this.CreateEntity(wrapperToUse);
            if (wrapperToUse.Entity == null)
            {
                throw new Exception("Entity was not set on wrapper");
            }
            wrapperToUse.IsUsed = true;
            this.EntitiesToDelete.Add(wrapperToUse.Entity);
            if (persisted)
            {
                using (var uow = this.UnitOfWorkFactory.CreateRequired())
                {
                    this.GetLayer<ISupportsInsert<TEntity>>().Insert(wrapperToUse.Entity);
                    uow.Commit();
                }
            }
            if (quantity == 2)
            {
                if (this.Wrapper1.IsUsed
                    && this.Wrapper2.IsUsed)
                {
                    throw new Exception("Cannot use more than 2 wrappers in a single test");
                }
                wrapperToUse = this.Wrapper1.IsUsed ? this.Wrapper2 : this.Wrapper1;
                entities.Wrapper2 = wrapperToUse;

                this.CreateEntity(wrapperToUse);
                if (wrapperToUse.Entity == null)
                {
                    throw new Exception("Entity was not set on wrapper");
                }
                wrapperToUse.IsUsed = true;
                this.EntitiesToDelete.Add(wrapperToUse.Entity);
                if (persisted)
                {
                    using (var uow = this.UnitOfWorkFactory.CreateRequired())
                    {
                        this.GetLayer<ISupportsInsert<TEntity>>().Insert(wrapperToUse.Entity);
                        uow.Commit();
                    }
                }
            }

            return entities;
        }
        protected virtual TId GetNullId()
        {
            return default(TId);
        }
        protected virtual void AssertExpectationsAfterUpdate(TEntityWrapper entityWrapper, TEntity entityFromDb)
        {
            this.AssertCommonPersistedEntityExpectations(entityWrapper, entityFromDb);
            this.OnAssertExpectationsAfterUpdate(entityWrapper, entityWrapper.Entity, entityFromDb);
        }
        protected virtual void AssertExpectationsAfterInsert(TEntityWrapper entityWrapper, TEntity entityFromDb)
        {
            this.AssertCommonPersistedEntityExpectations(entityWrapper, entityFromDb);
            this.OnAssertExpectationsAfterInsert(entityWrapper, entityWrapper.Entity, entityFromDb);
        }
        protected virtual void AssertExpectationsAfterDelete(TEntityWrapper entityWrapper)
        {
            entityWrapper.Entity.Id.IsNullId().Should().BeFalse(); // we should still have the old id

            this.GetFromDatabase(entityWrapper.Entity.Id).Should().BeNull();

            this.OnAssertExpectationsAfterDelete(entityWrapper);
        }
        protected virtual void AssertCommonPersistedEntityExpectations(TEntityWrapper entityWrapper, TEntity entityFromDb)
        {
            var entity = entityWrapper.Entity;

            entity.IsPersisted.Should().BeTrue();
            entityFromDb.Should().NotBeNull();
            entityFromDb.IsPersisted.Should().BeTrue();
            entityFromDb.Id.Should().Be(entity.Id);
            entityFromDb.Should().Be(entity);

            if (entity is IAuditable)
            {
                var auditable = (IAuditable)entity;
                var auditableFromDb = (IAuditable)entityFromDb;

                auditable.CreatedDate.Should().HaveValue();
                auditable.CreatedDate.Value.Should().NotBe(default(DateTime));
                auditable.CreatedUserId.Should().NotBeNullOrEmpty();
                //auditable.CreatedUserId.Value.Should().NotBe(default(Guid));
                auditable.UpdatedDate.Should().HaveValue();
                auditable.UpdatedDate.Value.Should().NotBe(default(DateTime));
                auditable.UpdatedUserId.Should().NotBeNullOrEmpty();
                //auditable.UpdatedUserId.Value.Should().NotBe(default(Guid));

                auditableFromDb.CreatedDate.Should().Be(auditable.CreatedDate);
                auditableFromDb.CreatedUserId.Should().Be(auditable.CreatedUserId);
                auditableFromDb.UpdatedDate.Should().Be(auditable.UpdatedDate);
                auditableFromDb.UpdatedUserId.Should().Be(auditable.UpdatedUserId);
            }

            this.OnAssertCommonPersistedEntityExpectations(entityWrapper, entityWrapper.Entity, entityFromDb);
        }
        protected virtual void UpdateEntity(TEntityWrapper entityWrapper, bool persistChanges)
        {
            this.ChangePropertyValues(entityWrapper, entityWrapper.Entity);
            if (persistChanges)
            {
                using (var uow = this.UnitOfWorkFactory.CreateRequired())
                {
                    this.GetLayer<ISupportsUpdate<TEntity>>().Update(entityWrapper.Entity);
                    uow.Commit();
                }
                entityWrapper.IsUpdated = true;
            }
        }

        protected virtual TEntity GetFromDatabase(TId id)
        {
            return this.CreateNewDbContext().Set<TEntity>().Find(id);
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public virtual void TestInsert_ValidationGetsCalled()
        {
            var entities = this.CreateEntities(1, false);
            this.ChangePropertyValuesToFailValidation(entities.Wrapper1, entities.Wrapper1.Entity);

            using (var uow = this.UnitOfWorkFactory.CreateRequired())
            {
                this.GetLayer<ISupportsInsert<TEntity>>().Insert(entities.Wrapper1.Entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public virtual void TestUpdate_ValidationGetsCalled()
        {
            var entities = this.CreateEntities(1, true);
            this.UpdateEntity(entities.Wrapper1, false);
            this.ChangePropertyValuesToFailValidation(entities.Wrapper1, entities.Wrapper1.Entity);

            using (var uow = this.UnitOfWorkFactory.CreateRequired())
            {
                this.GetLayer<ISupportsUpdate<TEntity>>().Update(entities.Wrapper1.Entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public virtual void TestSave_Insert_ValidationGetsCalled()
        {
            var entities = this.CreateEntities(1, false);
            this.ChangePropertyValuesToFailValidation(entities.Wrapper1, entities.Wrapper1.Entity);

            using (var uow = this.UnitOfWorkFactory.CreateRequired())
            {
                this.GetLayer<ISupportsSave<TEntity>>().Save(entities.Wrapper1.Entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ValidationException))]
        public virtual void TestSave_Update_ValidationGetsCalled()
        {
            var entities = this.CreateEntities(1, true);
            this.UpdateEntity(entities.Wrapper1, false);
            this.ChangePropertyValuesToFailValidation(entities.Wrapper1, entities.Wrapper1.Entity);

            using (var uow = this.UnitOfWorkFactory.CreateRequired())
            {
                this.GetLayer<ISupportsSave<TEntity>>().Save(entities.Wrapper1.Entity);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestDelete_ValidationDoesNotGetCalled()
        {
            var entities = this.CreateEntities(1, true);
            this.ChangePropertyValuesToFailValidation(entities.Wrapper1, entities.Wrapper1.Entity);

            using (var uow = this.UnitOfWorkFactory.CreateRequired())
            {
                this.GetLayer<ISupportsDelete<TEntity>>().Delete(entities.Wrapper1.Entity);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestGet()
        {
            var entities = this.CreateEntities(1, true);

            var entityFromDb = this.GetLayer<ISupportsGetById<TEntity, TId>>().Get(entities.Wrapper1.Entity.Id);

            this.AssertExpectationsAfterInsert(entities.Wrapper1, entityFromDb);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestGet_NonPersistedId()
        {
            this.GetLayer<ISupportsGetById<TEntity, TId>>().Get(this.GetNullId());
        }

        [Test]
        public virtual void TestGet_NonExistentId()
        {
            var entities = this.CreateEntities(1, true);

            this.GetLayer<ISupportsGetById<TEntity, TId>>().Get(this.IncrementId(entities.Wrapper1.Entity.Id)).Should().BeNull();
        }

        [Test]
        public virtual void TestInsert()
        {
            var entities = this.CreateEntities(1, true);

            entities.Wrapper1.Entity.IsPersisted.Should().BeTrue();

            var entityFromDb = this.GetFromDatabase(entities.Wrapper1.Entity.Id);

            this.AssertExpectationsAfterInsert(entities.Wrapper1, entityFromDb);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestInsert_Null()
        {
            TEntity entity = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsInsert<TEntity>>().Insert(entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestInsert_Persisted()
        {
            var entities = this.CreateEntities(1, true);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsInsert<TEntity>>().Insert(entities.Wrapper1.Entity);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestInsert_List()
        {
            var entities = this.CreateEntities(2, false);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(entities.Wrapper1.Entity);
            entityList.Add(entities.Wrapper2.Entity);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsInsertList<TEntity>>().Insert(entityList);
                uow.Commit();
            }

            entities.Wrapper1.Entity.IsPersisted.Should().BeTrue();
            entities.Wrapper2.Entity.IsPersisted.Should().BeTrue();

            var entityFromDb1 = this.GetFromDatabase(entities.Wrapper1.Entity.Id);
            var entityFromDb2 = this.GetFromDatabase(entities.Wrapper2.Entity.Id);

            this.AssertExpectationsAfterInsert(entities.Wrapper1, entityFromDb1);
            this.AssertExpectationsAfterInsert(entities.Wrapper2, entityFromDb2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestInsert_List_Null()
        {
            List<TEntity> entityList = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsInsertList<TEntity>>().Insert(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestInsert_List_Empty()
        {
            List<TEntity> entityList = new List<TEntity>();
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsInsertList<TEntity>>().Insert(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestInsert_List_WithPersisted()
        {
            var entities = this.CreateEntities(1, true);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(entities.Wrapper1.Entity);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsInsertList<TEntity>>().Insert(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestInsert_List_WithNull()
        {
            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(null);
            entityList.Add(this.CreateEntities(1, false).Wrapper1.Entity);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsInsertList<TEntity>>().Insert(entityList);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestExists()
        {
            var entities = this.CreateEntities(1, true);
            this.GetLayer<ISupportsExists<TId>>().Exists(entities.Wrapper1.Entity.Id).Should().BeTrue();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestExists_NonPersistedId()
        {
            this.GetLayer<ISupportsExists<TId>>().Exists(this.GetNullId());
        }

        [Test]
        public virtual void TestExists_NonExistentId()
        {
            var entities = this.CreateEntities(1, true);
            this.GetLayer<ISupportsExists<TId>>().Exists(this.IncrementId(entities.Wrapper1.Entity.Id)).Should().BeFalse();
        }

        [Test]
        public virtual void TestGetAll()
        {
            var entities = this.CreateEntities(1, true);

            var list = this.GetLayer<ISupportsGetAll<TEntity>>().GetAll();
            list.Should().NotBeNull();
            list.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Test]
        public virtual void TestGetQueryable()
        {
            var entities = this.CreateEntities(1, true);

            var id = entities.Wrapper1.Entity.Id;

            var list = from o in this.GetLayer<ISupportsQueryable<TEntity>>().GetQueryable()
                       where o.Id.Equals(id)
                       select o;

            int i = 0;
            foreach (var entityFromDb in list)
            {
                i++;
                entityFromDb.Id.Should().Be(id);
            }
            i.Should().Be(1);
        }

        [Test]
        public virtual void TestGetTotalCount()
        {
            int expectedCount = this.GetLayer<ISupportsQueryable<TEntity>>().GetQueryable().Count();
            this.GetLayer<ISupportsGetTotalCount>().GetTotalCount(new LinqExpressionSearchCriteria<TEntity>()).Should().Be(expectedCount);

            var entities = this.CreateEntities(1, true);

            var entityId = entities.Wrapper1.Entity.Id;

            expectedCount += 1;
            this.GetLayer<ISupportsGetTotalCount>().GetTotalCount(new LinqExpressionSearchCriteria<TEntity>()).Should().Be(expectedCount);

            this.GetLayer<ISupportsGetTotalCount>().GetTotalCount(new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(entityId))).
                Should().Be(1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestGetTotalCount_Null()
        {
            this.GetLayer<ISupportsGetTotalCount>().GetTotalCount(null);
        }

        [Test]
        public virtual void TestUpdate()
        {
            var entities = this.CreateEntities(1, true);

            this.UpdateEntity(entities.Wrapper1, true);

            var entityFromDb = this.GetFromDatabase(entities.Wrapper1.Entity.Id);

            this.AssertExpectationsAfterUpdate(entities.Wrapper1, entityFromDb);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestUpdate_Null()
        {
            TEntity entity = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsUpdate<TEntity>>().Update(entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestUpdate_NotPersisted()
        {
            var entities = this.CreateEntities(1, false);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsUpdate<TEntity>>().Update(entities.Wrapper1.Entity);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestUpdate_List()
        {
            var entities = this.CreateEntities(2, true);
            this.UpdateEntity(entities.Wrapper1, false);
            this.UpdateEntity(entities.Wrapper2, false);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(entities.Wrapper1.Entity);
            entityList.Add(entities.Wrapper2.Entity);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsUpdateList<TEntity>>().Update(entityList);
                uow.Commit();
            }

            var entityFromDb1 = this.GetFromDatabase(entities.Wrapper1.Entity.Id);
            var entityFromDb2 = this.GetFromDatabase(entities.Wrapper2.Entity.Id);

            this.AssertExpectationsAfterUpdate(entities.Wrapper1, entityFromDb1);
            this.AssertExpectationsAfterUpdate(entities.Wrapper2, entityFromDb2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestUpdate_List_Null()
        {
            List<TEntity> entityList = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsUpdateList<TEntity>>().Update(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestUpdate_List_Empty()
        {
            List<TEntity> entityList = new List<TEntity>();
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsUpdateList<TEntity>>().Update(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestUpdate_List_WithNonPersisted()
        {
            var entities = this.CreateEntities(1, false);
            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(entities.Wrapper1.Entity);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsUpdateList<TEntity>>().Update(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestUpdate_List_WithNull()
        {
            var entities = this.CreateEntities(1, true);
            this.UpdateEntity(entities.Wrapper1, false);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(null);
            entityList.Add(entities.Wrapper1.Entity);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsUpdateList<TEntity>>().Update(entityList);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestDelete()
        {
            var entities = this.CreateEntities(1, true);

            var id = entities.Wrapper1.Entity.Id;

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDelete<TEntity>>().Delete(entities.Wrapper1.Entity);
                uow.Commit();
            }

            this.AssertExpectationsAfterDelete(entities.Wrapper1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestDelete_NotPersisted()
        {
            var entities = this.CreateEntities(1, false);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDelete<TEntity>>().Delete(entities.Wrapper1.Entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestDelete_Null()
        {
            TEntity entity = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDelete<TEntity>>().Delete(entity);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestDelete_List()
        {
            var entities = this.CreateEntities(2, true);

            var entity1Id = entities.Wrapper1.Entity.Id;
            var entity2Id = entities.Wrapper2.Entity.Id;

            var entityList = new List<TEntity>();
            entityList.Add(entities.Wrapper1.Entity);
            entityList.Add(entities.Wrapper2.Entity);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteList<TEntity>>().Delete(entityList);
                uow.Commit();
            }

            this.AssertExpectationsAfterDelete(entities.Wrapper1);
            this.AssertExpectationsAfterDelete(entities.Wrapper2);

            //this.GetFromDatabase(entity1Id).Should().BeNull();
            //this.GetFromDatabase(entity2Id).Should().BeNull();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestDelete_List_Null()
        {
            List<TEntity> entityList = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteList<TEntity>>().Delete(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestDelete_List_Empty()
        {
            List<TEntity> entityList = new List<TEntity>();
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteList<TEntity>>().Delete(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestDelete_List_WithNonPersisted()
        {
            var entities = this.CreateEntities(1, false);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(entities.Wrapper1.Entity);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteList<TEntity>>().Delete(entityList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestDelete_List_WithNull()
        {
            var entities = this.CreateEntities(1, true);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(null);
            entityList.Add(entities.Wrapper1.Entity);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteList<TEntity>>().Delete(entityList);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestDelete_Search()
        {
            var entities = this.CreateEntities(2, true);

            var entity1Id = entities.Wrapper1.Entity.Id;
            var entity2Id = entities.Wrapper2.Entity.Id;

            LinqExpressionSearchCriteria<TEntity> criteria = new LinqExpressionSearchCriteria<TEntity>();
            criteria.WhereExpression = o => o.Id.Equals(entity1Id);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteSearch>().Delete(criteria);
                uow.Commit();
            }

            this.AssertExpectationsAfterDelete(entities.Wrapper1);

            //this.GetFromDatabase(entity1Id).Should().BeNull();

            this.GetFromDatabase(entity2Id).Should().NotBeNull();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestDelete_Search_Null()
        {
            ISearchCriteria searchCriteria = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteSearch>().Delete(searchCriteria);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestDeleteById()
        {
            var entities = this.CreateEntities(1, true);

            var id = entities.Wrapper1.Entity.Id;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteById<TId>>().DeleteById(id);
                uow.Commit();
            }

            this.AssertExpectationsAfterDelete(entities.Wrapper1);

            //var entityFromDb = this.GetFromDatabase(id);
            //entityFromDb.Should().BeNull();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestDeleteById_NonPersisted()
        {
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteById<TId>>().DeleteById(this.GetNullId());
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestDeleteById_NonExistent()
        {
            var entities = this.CreateEntities(1, true);

            var id = entities.Wrapper1.Entity.Id;

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteById<TId>>().DeleteById(this.IncrementId(id));
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestDeleteById_List()
        {
            var entities = this.CreateEntities(2, true);

            var entity1Id = entities.Wrapper1.Entity.Id;
            var entity2Id = entities.Wrapper2.Entity.Id;

            var entityIdList = new List<TId>();
            entityIdList.Add(entity1Id);
            entityIdList.Add(entity2Id);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteListByIds<TId>>().DeleteById(entityIdList);
                uow.Commit();
            }

            this.AssertExpectationsAfterDelete(entities.Wrapper1);
            this.AssertExpectationsAfterDelete(entities.Wrapper2);

            //this.GetFromDatabase(entity1Id).Should().BeNull();
            //this.GetFromDatabase(entity2Id).Should().BeNull();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestDeleteById_List_Null()
        {
            List<TId> entityIdList = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteListByIds<TId>>().DeleteById(entityIdList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestDeleteById_List_Empty()
        {
            List<TId> entityIdList = new List<TId>();
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteListByIds<TId>>().DeleteById(entityIdList);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestDeleteById_List_WithNonPersisted()
        {
            List<TId> entityIdList = new List<TId>();
            entityIdList.Add(this.GetNullId());
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteListByIds<TId>>().DeleteById(entityIdList);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestDeleteById_List_WithNonExistent()
        {
            var entities = this.CreateEntities(1, true);

            var id = entities.Wrapper1.Entity.Id;

            List<TId> entityIdList = new List<TId>();
            entityIdList.Add(this.IncrementId(id));
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsDeleteListByIds<TId>>().DeleteById(entityIdList);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestSave_Insert()
        {
            var entities = this.CreateEntities(1, false);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSave<TEntity>>().Save(entities.Wrapper1.Entity);
                uow.Commit();
            }

            var entityFromDb = this.GetFromDatabase(entities.Wrapper1.Entity.Id);

            this.AssertExpectationsAfterInsert(entities.Wrapper1, entityFromDb);
        }

        [Test]
        public virtual void TestSave_List_Insert()
        {
            var entities = this.CreateEntities(2, false);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(entities.Wrapper1.Entity);
            entityList.Add(entities.Wrapper2.Entity);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSaveList<TEntity>>().Save(entityList);
                uow.Commit();
            }

            var entityFromDb1 = this.GetFromDatabase(entities.Wrapper1.Entity.Id);
            var entityFromDb2 = this.GetFromDatabase(entities.Wrapper2.Entity.Id);

            this.AssertExpectationsAfterInsert(entities.Wrapper1, entityFromDb1);
            this.AssertExpectationsAfterInsert(entities.Wrapper2, entityFromDb2);

        }

        [Test]
        public virtual void TestSave_List_InsertAndUpdate()
        {
            var entitiesSet1 = this.CreateEntities(1, true);
            var entitiesSet2 = this.CreateEntities(1, false);

            this.UpdateEntity(entitiesSet1.Wrapper1, false);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(entitiesSet1.Wrapper1.Entity);
            entityList.Add(entitiesSet2.Wrapper1.Entity);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSaveList<TEntity>>().Save(entityList);
                uow.Commit();
            }

            var entityFromDb1 = this.GetFromDatabase(entitiesSet1.Wrapper1.Entity.Id);
            var entityFromDb2 = this.GetFromDatabase(entitiesSet2.Wrapper1.Entity.Id);

            this.AssertExpectationsAfterUpdate(entitiesSet1.Wrapper1, entityFromDb1);
            this.AssertExpectationsAfterInsert(entitiesSet2.Wrapper1, entityFromDb2);
        }

        [Test]
        public virtual void TestSave_Update()
        {
            var entities = this.CreateEntities(1, true);

            this.UpdateEntity(entities.Wrapper1, false);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSave<TEntity>>().Save(entities.Wrapper1.Entity);
                uow.Commit();
            }

            var entityFromDb = this.GetFromDatabase(entities.Wrapper1.Entity.Id);

            this.AssertExpectationsAfterUpdate(entities.Wrapper1, entityFromDb);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestSave_Null()
        {
            TEntity entity = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSave<TEntity>>().Save(entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestSave_List_Null()
        {
            List<TEntity> entity = null;
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSaveList<TEntity>>().Save(entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestSave_List_Empty()
        {
            List<TEntity> entity = new List<TEntity>();
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSaveList<TEntity>>().Save(entity);
                uow.Commit();
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public virtual void TestSave_List_WithNull()
        {
            List<TEntity> entity = new List<TEntity>();
            entity.Add(null);
            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSaveList<TEntity>>().Save(entity);
                uow.Commit();
            }
        }

        [Test]
        public virtual void TestSave_List_Update()
        {
            var entities = this.CreateEntities(2, true);
            this.UpdateEntity(entities.Wrapper1, false);
            this.UpdateEntity(entities.Wrapper2, false);

            List<TEntity> entityList = new List<TEntity>();
            entityList.Add(entities.Wrapper1.Entity);
            entityList.Add(entities.Wrapper2.Entity);

            using (var uow = this.UnitOfWorkFactory.Create())
            {
                this.GetLayer<ISupportsSaveList<TEntity>>().Save(entityList);
                uow.Commit();
            }

            var entityFromDb1 = this.GetFromDatabase(entities.Wrapper1.Entity.Id);
            var entityFromDb2 = this.GetFromDatabase(entities.Wrapper2.Entity.Id);

            this.AssertExpectationsAfterUpdate(entities.Wrapper1, entityFromDb1);
            this.AssertExpectationsAfterUpdate(entities.Wrapper2, entityFromDb2);
        }

        [Test]
        public virtual void TestCreate()
        {
            var type = typeof(TEntity);
            if (!type.IsAbstract)
            {
                var entity = this.GetLayer<ISupportsCreate<TEntity>>().Create();
                this.EntitiesToDelete.Add(entity);
                entity.Should().NotBeNull();
                entity.IsPersisted.Should().BeFalse();
                entity.Should().BeAssignableTo<TEntity>();
            }
        }

        [Test]
        public virtual void TestCreate_Generic()
        {
            var type = typeof(TEntity);
            if (!type.IsAbstract)
            {
                var entity = this.GetLayer<ISupportsCreate<TEntity>>().Create<TEntity>();
                this.EntitiesToDelete.Add(entity);
                entity.Should().NotBeNull();
                entity.IsPersisted.Should().BeFalse();
                entity.Should().BeAssignableTo<TEntity>();
            }
        }

        [Test]
        public virtual void TestSearch()
        {
            var entities = this.CreateEntities(2, true);

            var entityId1 = entities.Wrapper1.Entity.Id;
            var entityId2 = entities.Wrapper2.Entity.Id;
            var nonExistentEntityId = this.IncrementId(entityId2);

            var criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(nonExistentEntityId));
            var searchResult = this.GetLayer<ISupportsSearch<TEntity>>().Search(criteria);
            searchResult.Should().NotBeNull();
            searchResult.SearchCriteria.Should().BeSameAs(criteria);
            searchResult.TotalCount.Should().Be(0);
            searchResult.Results.Should().NotBeNull();
            searchResult.Results.Count.Should().Be(0);

            criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(entityId1));
            searchResult = this.GetLayer<ISupportsSearch<TEntity>>().Search(criteria);
            searchResult.Should().NotBeNull();
            searchResult.SearchCriteria.Should().BeSameAs(criteria);
            searchResult.TotalCount.Should().Be(1);
            searchResult.Results.Should().NotBeNull();
            searchResult.Results.Count.Should().Be(1);
            searchResult.Results.ElementAt(0).Should().Be(entities.Wrapper1.Entity);

            criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(entityId1) || o.Id.Equals(entityId2));
            searchResult = this.GetLayer<ISupportsSearch<TEntity>>().Search(criteria);
            searchResult.Should().NotBeNull();
            searchResult.SearchCriteria.Should().BeSameAs(criteria);
            searchResult.TotalCount.Should().Be(2);
            searchResult.Results.Should().NotBeNull();
            searchResult.Results.Count.Should().Be(2);
            searchResult.Results.Contains(entities.Wrapper1.Entity).Should().BeTrue();
            searchResult.Results.Contains(entities.Wrapper2.Entity).Should().BeTrue();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestSearch_Null()
        {
            ISearchCriteria searchCriteria = null;
            this.GetLayer<ISupportsSearch<TEntity>>().Search(searchCriteria);
        }

        [Test]
        public virtual void TestSearchUnique()
        {
            var entities = this.CreateEntities(2, true);

            var entityId1 = entities.Wrapper1.Entity.Id;
            var entityId2 = entities.Wrapper2.Entity.Id;
            var nonExistentEntityId = this.IncrementId(entityId2);

            var criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(nonExistentEntityId));
            var entityFromDb = this.GetLayer<ISupportsSearchUnique<TEntity>>().SearchUnique(criteria);
            entityFromDb.Should().BeNull();

            criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(entityId1));
            entityFromDb = this.GetLayer<ISupportsSearchUnique<TEntity>>().SearchUnique(criteria);
            entityFromDb.Should().NotBeNull();
            entityFromDb.Id.Should().Be(entities.Wrapper1.Entity.Id);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestSearchUnique_Null()
        {
            ISearchCriteria searchCriteria = null;
            this.GetLayer<ISupportsSearchUnique<TEntity>>().SearchUnique(searchCriteria);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public virtual void TestSearchUnique_NotUnique()
        {
            var entities = this.CreateEntities(2, true);

            var entityId1 = entities.Wrapper1.Entity.Id;
            var entityId2 = entities.Wrapper2.Entity.Id;

            var criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(entityId1) || o.Id.Equals(entityId2));
            this.GetLayer<ISupportsSearchUnique<TEntity>>().SearchUnique(criteria);
        }
    }
}
