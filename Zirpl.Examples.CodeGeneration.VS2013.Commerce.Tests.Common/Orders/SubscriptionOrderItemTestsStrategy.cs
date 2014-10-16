using System;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Orders
{
    public partial class SubscriptionOrderItemEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Order Order { get; set; }
        public DisplayProduct DisplayProduct { get; set; }
        public SubscriptionPeriod SubscriptionPeriod { get; set; }
        public SubscriptionInstance ResultingSubscriptionInstance { get; set; }
    }

    public partial class SubscriptionOrderItemTestsStrategy
    {
        public virtual void SetUpWrapper(SubscriptionOrderItemEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(SubscriptionOrderItemEntityWrapper wrapper)
        {
            var customer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.Visitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Order = this.Data.CreateOrder(customer, this.Data.CreateStripeCustomerChargeOption(customer, this.Data.CreateAddress(this.Data.GetExistingStateProvince())));
            wrapper.DisplayProduct = this.Data.CreateDisplayProduct();
            wrapper.SubscriptionPeriod = this.Data.CreateSubscriptionPeriod();
            wrapper.Entity = this.Data.CreateSubscriptionInitialOrderItem(wrapper.Order, wrapper.DisplayProduct, wrapper.SubscriptionPeriod);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            SubscriptionOrderItemEntityWrapper entityWrapper, SubscriptionOrderItem entity, SubscriptionOrderItem entityFromDb)
        {
            entityFromDb.Quantity.Should().Be(entity.Quantity);
            entityFromDb.ItemAmountBeforeDiscount.Should().Be(entity.ItemAmountBeforeDiscount);
            entityFromDb.OriginalItemAmount.Should().Be(entity.OriginalItemAmount);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DisplayProduct, o => o.DisplayProductId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Order, o => o.OrderId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.SubscriptionPeriod, o => o.SubscriptionPeriodId);
            entityFromDb.DisplayProduct.Should().Be(entityWrapper.DisplayProduct);
            entityFromDb.Order.Should().Be(entityWrapper.Order);
            entityFromDb.SubscriptionPeriod.Should().Be(entityWrapper.SubscriptionPeriod);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            SubscriptionOrderItemEntityWrapper entityWrapper, SubscriptionOrderItem entity, SubscriptionOrderItem entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            SubscriptionOrderItemEntityWrapper entityWrapper, SubscriptionOrderItem entity, SubscriptionOrderItem entityFromDb)
        {
            entity.ResultingSubscriptionInstance.Should().NotBeNull();
            entity.ResultingSubscriptionInstance.Should().BeSameAs(entityWrapper.ResultingSubscriptionInstance);
        }

        public virtual void OnAssertExpectationsAfterDelete(SubscriptionOrderItemEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IOrderDataService>().Get(entityWrapper.Order.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<ISubscriptionPeriodDataService>().Get(entityWrapper.SubscriptionPeriod.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(SubscriptionOrderItemEntityWrapper entityWrapper, SubscriptionOrderItem entity)
        {
            entityWrapper.ResultingSubscriptionInstance = this.Data.CreateSubscriptionInstance(this.Data.CreateSubscription(entity, entity.Order.ShippingAddress, entity.Order.CustomerChargeOption), entity);
            entity.ResultingSubscriptionInstance = entityWrapper.ResultingSubscriptionInstance;
        }

        public virtual void ChangePropertyValuesToFailValidation(SubscriptionOrderItemEntityWrapper entityWrapper, SubscriptionOrderItem entity)
        {
            entity.Quantity = -1;
        }
    }
}
