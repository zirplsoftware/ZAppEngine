using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;
using Zirpl.Testing;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Promotions
{
    public partial class DiscountEntityWrapper
    {
        public DisplayProduct DisplayProduct1 { get; set; }
        public DisplayProduct DisplayProduct2 { get; set; }
        public PromoCode PromoCode { get; set; }
    }

    [TestFixture]
    public partial class DiscountTestsStrategy
    {
        public virtual void SetUpWrapper(DiscountEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(DiscountEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateDiscount(this.Data.CreatePromoCode());
            wrapper.DisplayProduct1 = this.Data.CreateDisplayProduct();
            wrapper.PromoCode = wrapper.Entity.PromoCode;
            wrapper.Entity.AppliesToDisplayProducts.Add(wrapper.DisplayProduct1);
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(DiscountEntityWrapper entityWrapper, Discount entity, Discount entityFromDb)
        {
            entityFromDb.Amount.Should().Be(entity.Amount);
            entityFromDb.Name.Should().Be(entity.Name);
            entityFromDb.DiscountUsageRestrictionQuantity.Should().Be(entity.DiscountUsageRestrictionQuantity);
            entityFromDb.EndDate.Should().Be(entity.EndDate);
            entityFromDb.StartDate.Should().Be(entity.StartDate);
            entityFromDb.Published.Should().Be(entity.Published);
            entityFromDb.StopAfterChargeCyles.Should().Be(entity.StopAfterChargeCyles);
            entityFromDb.AppliesToDisplayProducts.Count().Should().BeGreaterOrEqualTo(1);
            entityFromDb.AppliesToDisplayProducts.Count().Should().BeLessOrEqualTo(2);
            entityFromDb.AppliesToDisplayProducts.Count().Should().Be(entity.AppliesToDisplayProducts.Count);
            foreach (var displayProduct in entityFromDb.AppliesToDisplayProducts)
            {
                displayProduct.IsPersisted.Should().BeTrue();
            }
            entityFromDb.AppliesToDisplayProducts.OrderByDescending(o => o.Id).First().Id.Should().Be(
                entity.AppliesToDisplayProducts.OrderByDescending(o => o.Id).First().Id);

            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DiscountAmountType, o => o.DiscountAmountTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DiscountApplicabilityType, o => o.DiscountApplicabilityTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DiscountUsageRestrictionType, o => o.DiscountUsageRestrictionTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.PromoCode, o => o.PromoCodeId);
        }

        public virtual void OnAssertExpectationsAfterInsert(DiscountEntityWrapper entityWrapper, Discount entity, Discount entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(DiscountEntityWrapper entityWrapper, Discount entity, Discount entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(DiscountEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IPromoCodeDataService>().Get(entityWrapper.PromoCode.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct1.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct2.Id).Should().NotBeNull(); 
            }
        }

        public virtual void ChangePropertyValues(DiscountEntityWrapper entityWrapper, Discount entity)
        {
            entity.Amount += entity.Amount;
            entity.Name = Guid.NewGuid().ToString();
            entity.DiscountUsageRestrictionQuantity = 2;
            entity.EndDate = DateTime.Now.AddDays(3);
            entity.StartDate = DateTime.Now.AddDays(1);
            entity.Published = true;
            entity.StopAfterChargeCyles = 222;
            entityWrapper.DisplayProduct1 = this.Data.CreateDisplayProduct();
            entity.AppliesToDisplayProducts.Add(entityWrapper.DisplayProduct1);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void ChangePropertyValuesToFailValidation(DiscountEntityWrapper entityWrapper, Discount entity)
        {
            entity.Amount = 0;
        }
    }
}
