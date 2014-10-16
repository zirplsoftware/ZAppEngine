using System;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Orders
{
    public partial class DiscountUsageEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Order Order { get; set; }
        public Discount Discount { get; set; }
        //public OrderItem OrderItem { get; set; }
    }

    public partial class DiscountUsageTestsStrategy
    {
        public virtual void SetUpWrapper(DiscountUsageEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(DiscountUsageEntityWrapper wrapper)
        {
            var customer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.Visitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Order = this.Data.CreateOrder(customer, this.Data.CreateStripeCustomerChargeOption(customer, this.Data.CreateAddress(this.Data.GetExistingStateProvince())));
            wrapper.Discount = this.Data.CreateDiscount(this.Data.CreatePromoCode());
            wrapper.Entity = this.Data.CreateDiscountUsage(wrapper.Order, wrapper.Discount);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            DiscountUsageEntityWrapper entityWrapper, DiscountUsage entity, DiscountUsage entityFromDb)
        {
            entityFromDb.DateUsed.Should().Be(entity.DateUsed);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Discount, o => o.DiscountId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Order, o => o.OrderId);
            entityFromDb.Discount.Should().Be(entityWrapper.Discount);
            entityFromDb.Order.Should().Be(entityWrapper.Order);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            DiscountUsageEntityWrapper entityWrapper, DiscountUsage entity, DiscountUsage entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            DiscountUsageEntityWrapper entityWrapper, DiscountUsage entity, DiscountUsage entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(DiscountUsageEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IDiscountDataService>().Get(entityWrapper.Discount.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IOrderDataService>().Get(entityWrapper.Order.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(DiscountUsageEntityWrapper entityWrapper, DiscountUsage entity)
        {
        }

        public virtual void ChangePropertyValuesToFailValidation(DiscountUsageEntityWrapper entityWrapper, DiscountUsage entity)
        {
            entity.DateUsed = DateTime.MinValue;
        }
    }
}
