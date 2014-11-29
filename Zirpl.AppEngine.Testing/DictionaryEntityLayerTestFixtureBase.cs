using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.Ioc;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.EntityFramework;
using Zirpl.AppEngine.Model.Search;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Testing;
using Zirpl.AppEngine.Web.Mvc.Ioc.Autofac;

namespace Zirpl.AppEngine.Testing
{
    [TestFixture]
    public abstract class DictionaryEntityLayerTestFixtureBase<TEntity, TId, TEnum>
        where TEntity : class, IStaticLookup<TId, TEnum>
        where TEnum : struct
        where TId : struct, IEquatable<TId>
    {
        public IDependencyResolver DependencyResolver { get { return IocUtils.DependencyResolver; } }
        protected abstract TSupports GetLayer<TSupports>() where TSupports : class, ISupports;
        
        protected abstract DbContextBase CreateNewDbContext();

        protected virtual TEntity GetFromDatabase(TId id)
        {
            return this.CreateNewDbContext().Set<TEntity>().Find(id);
        }

        //ISupportsGetById<TEntity, TId>, 
        //ISupportsGetAll<TEntity>, 
        //ISupportsExists<TId>, 
        //ISupportsGetTotalCount, 
        //ISupportsSearch<TEntity>, 
        //ISupportsSearchUnique<TEntity>, 
        //ISupportsQueryable<TEntity>

        protected virtual TId GetExistentId()
        {
            return this.GetAllIds().First();
        }

        protected virtual Object GetNonExistentId()
        {
            var lastId = (from id in this.GetAllIds()
                    orderby id
                    select id).Last();
            Object lastIdObj = lastId;

            if (lastIdObj is Int32)
            {
                lastIdObj = ((Int32) lastIdObj) + 1;
            }
            else if (lastIdObj is byte)
            {
                lastIdObj = ((byte) lastIdObj) + 1;
                lastIdObj = Convert.ToByte((Int32)lastIdObj);
            }
            else
            {
                throw new Exception("Could not increment id");
            }
            return (TId)lastIdObj;
        }

        protected virtual IdWrapper<TId> Get2ExistentIds()
        {
            var lastIds = (from id in this.GetAllIds()
                          orderby id
                          select id).Take(2);
            return new IdWrapper<TId>() { Id1 = lastIds.First(), Id2 = lastIds.Last() };
        }

        protected virtual IEnumerable<TId> GetAllIds()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TId>();
        }

        protected virtual TId GetNullId()
        {
            return default(TId);
        }

        protected virtual void AssertEntity(TEntity entity)
        {
            var entityFromDb = this.GetFromDatabase(entity.Id);
            entity.Should().NotBeNull();
            entity.Name.Should().NotBeNull();
            entity.Id.IsNullId().Should().BeFalse();
        }

        [Test]
        public virtual void TestGet()
        {
            var id = this.GetExistentId();
            var entity = this.GetLayer<ISupportsGetById<TEntity, TId>>().Get(id);
            this.AssertEntity(entity);
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
            var id = (TId)this.GetNonExistentId();
            var entity = this.GetLayer<ISupportsGetById<TEntity, TId>>().Get(id);
            entity.Should().BeNull();
        }

        [Test]
        public virtual void TestExists()
        {
            var id = this.GetExistentId();
            this.GetLayer<ISupportsExists<TId>>().Exists(id).Should().BeTrue();
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
            var id = (TId)this.GetNonExistentId();
            this.GetLayer<ISupportsExists<TId>>().Exists(id).Should().BeFalse();
        }

        [Test]
        public virtual void TestGetAll()
        {
            var list = this.GetLayer<ISupportsGetAll<TEntity>>().GetAll();
            list.Should().NotBeNull();
            list.Count.Should().BeGreaterOrEqualTo(1);
            foreach (var entity in list)
            {
                this.AssertEntity(entity);
            }

            var ids = (from o in list
                      select o.Id).ToArray();
            foreach (TId id in ids)
            {
                this.GetAllIds().Contains(id);
            }
            foreach (TId id in this.GetAllIds())
            {
                ids.Contains(id);
            }
            ids.Count().Should().Be(this.GetAllIds().Count());
        }

        [Test]
        public virtual void TestGetQueryable()
        {
            var id = this.GetExistentId();
            var list = from o in this.GetLayer<ISupportsQueryable<TEntity>>().GetQueryable()
                       where o.Id.Equals(id)
                       select o;

            int i = 0;
            foreach (var entity in list)
            {
                i++;
                entity.Id.Should().Be(id);
                this.AssertEntity(entity);
            }
            i.Should().Be(1);
        }

        [Test]
        public virtual void TestGetTotalCount()
        {
            int expectedCount = this.GetLayer<ISupportsQueryable<TEntity>>().GetQueryable().Count();
            this.GetLayer<ISupportsGetTotalCount>().GetTotalCount(new LinqExpressionSearchCriteria<TEntity>()).Should().Be(expectedCount);

            // this makes sure the count is exactly the same as the number of enums
            expectedCount.Should().Be(this.GetAllIds().Count());

            var id = this.GetExistentId();
            this.GetLayer<ISupportsGetTotalCount>().GetTotalCount(new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(id))).
                Should().Be(1);

            id = (TId)this.GetNonExistentId();
            this.GetLayer<ISupportsGetTotalCount>().GetTotalCount(new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(id))).
                Should().Be(0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public virtual void TestGetTotalCount_Null()
        {
            this.GetLayer<ISupportsGetTotalCount>().GetTotalCount(null);
        }

        [Test]
        public virtual void TestSearch()
        {
            var entityId1 = this.GetExistentId();
            var nonExistentEntityId = (TId)this.GetNonExistentId();

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
            searchResult.Results.First().Id.Should().Be(entityId1);
            this.AssertEntity(searchResult.Results.First());

            if (this.GetAllIds().Count() > 1)
            {
                var entityId2 = this.Get2ExistentIds().Id2;

                criteria =
                    new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(entityId1) || o.Id.Equals(entityId2));
                searchResult = this.GetLayer<ISupportsSearch<TEntity>>().Search(criteria);
                searchResult.Should().NotBeNull();
                searchResult.SearchCriteria.Should().BeSameAs(criteria);
                searchResult.TotalCount.Should().Be(2);
                searchResult.Results.Should().NotBeNull();
                searchResult.Results.Count.Should().Be(2);
                searchResult.Results.OrderBy(o => o.Id).First().Id.Should().Be(entityId1);
                searchResult.Results.OrderBy(o => o.Id).Skip(1).First().Id.Should().Be(entityId2);
                foreach (var entity in searchResult.Results)
                {
                    this.AssertEntity(entity);
                }
            }
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
            var entityId1 = this.GetExistentId();
            //var entityId2 = entities.Wrapper2.Entity.Id;
            var nonExistentEntityId = (TId)this.GetNonExistentId();

            var criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(nonExistentEntityId));
            var entity = this.GetLayer<ISupportsSearchUnique<TEntity>>().SearchUnique(criteria);
            entity.Should().BeNull();

            criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(entityId1));
            entity = this.GetLayer<ISupportsSearchUnique<TEntity>>().SearchUnique(criteria);
            entity.Should().NotBeNull();
            this.AssertEntity(entity);
            entity.Id.Should().Be(entityId1);
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
            if (this.GetAllIds().Count() > 1)
            {
                var entityId1 = this.GetExistentId();
                var entityId2 = this.Get2ExistentIds().Id2;

                var criteria = new LinqExpressionSearchCriteria<TEntity>(o => o.Id.Equals(entityId1) || o.Id.Equals(entityId2));
                this.GetLayer<ISupportsSearchUnique<TEntity>>().SearchUnique(criteria);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
