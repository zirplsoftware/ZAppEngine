using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Catalog
{
    public partial class DisplayProductEntityWrapper
    {
        public UpdatableProperty<Discount> Discount = new UpdatableProperty<Discount>();
    }

    [TestFixture]
    public partial class DisplayProductTestsStrategy 
    {
        public virtual void SetUpWrapper(DisplayProductEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(DisplayProductEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateDisplayProduct();
            wrapper.Discount.Original = this.Data.CreateDiscount(this.Data.CreatePromoCode());
            wrapper.Entity.ApplicableDiscounts.Add(wrapper.Discount.Original);
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(DisplayProductEntityWrapper entityWrapper, DisplayProduct entity, DisplayProduct entityFromDb)
        {
            entityFromDb.AdminComment.Should().Be(entity.AdminComment);
            entityFromDb.Name.Should().Be(entity.Name);
            entityFromDb.ApplicableDiscounts.Count().Should().BeGreaterOrEqualTo(1);
            entityFromDb.ApplicableDiscounts.Count().Should().BeLessOrEqualTo(2);
            entityFromDb.ApplicableDiscounts.Count().Should().Be(entity.ApplicableDiscounts.Count);
            foreach (var discount in entityFromDb.ApplicableDiscounts)
            {
                discount.IsPersisted.Should().BeTrue();
            }
            entityFromDb.ApplicableDiscounts.OrderByDescending(o => o.Id).First().Id.Should().Be(
                entity.ApplicableDiscounts.OrderByDescending(o => o.Id).First().Id);
        }

        public virtual void OnAssertExpectationsAfterInsert(DisplayProductEntityWrapper entityWrapper, DisplayProduct entity, DisplayProduct entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(DisplayProductEntityWrapper entityWrapper, DisplayProduct entity, DisplayProduct entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(DisplayProductEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IDiscountDataService>().Get(entityWrapper.Discount.Original.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<IDiscountDataService>().Get(entityWrapper.Discount.Updated.Id).Should().NotBeNull(); 
            }
        }

        public virtual void ChangePropertyValues(DisplayProductEntityWrapper entityWrapper, DisplayProduct entity)
        {
            entity.Name = Guid.NewGuid().ToString();
            entity.AdminComment = Guid.NewGuid().ToString();
            entityWrapper.Discount.Updated = this.Data.CreateDiscount(this.Data.CreatePromoCode());
            this.DependencyResolver.Resolve<IDiscountDataService>().Insert(entityWrapper.Discount.Updated);
            entity.ApplicableDiscounts.Add(entityWrapper.Discount.Updated);
        }

        public virtual void ChangePropertyValuesToFailValidation(DisplayProductEntityWrapper entityWrapper, DisplayProduct entity)
        {
            entity.Name = null;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }
    }
}
