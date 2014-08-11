using System;
using System.Linq.Expressions;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Subscriptions
{
    public partial class SubscriptionInstanceEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Customer Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public StripeCustomerChargeOption CustomerChargeOption { get; set; }
        public SubscriptionOrderItem CreatedBySubscriptionOrderItem { get; set; }
        public Subscription Subscription { get; set; }
    }

    public partial class SubscriptionInstanceTestsStrategy
    {
        public virtual void SetUpWrapper(SubscriptionInstanceEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
            var visitor1 = wrapper.Visitor;
            var shippingAddress1 = this.Data.CreateAddress(this.Data.GetExistingStateProvince());
            var customer1 = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor1, shippingAddress1);
            var customerChargeOption1 = this.Data.CreateStripeCustomerChargeOption(customer1, this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            var order1 = this.Data.CreateOrder(customer1, customerChargeOption1);
            var orderItem1 = this.Data.CreateSubscriptionInitialOrderItem(order1, this.Data.CreateDisplayProduct(), this.Data.CreateSubscriptionPeriod());

            wrapper.Customer = customer1;
            wrapper.CustomerChargeOption = customerChargeOption1;
            wrapper.ShippingAddress = shippingAddress1;
            wrapper.CreatedBySubscriptionOrderItem = orderItem1;
        }

        public virtual void CreateEntity(SubscriptionInstanceEntityWrapper wrapper)
        {
            var subscription = this.Data.CreateSubscription(
                wrapper.CreatedBySubscriptionOrderItem,
                wrapper.ShippingAddress,
                wrapper.CustomerChargeOption);
            var subscriptionInstance = this.Data.CreateSubscriptionInstance(subscription, wrapper.CreatedBySubscriptionOrderItem);

            wrapper.Subscription = subscription;
            wrapper.Entity = subscriptionInstance;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            SubscriptionInstanceEntityWrapper entityWrapper, SubscriptionInstance entity, SubscriptionInstance entityFromDb)
        {
            entityFromDb.ShipmentsRemaining.Should().Be(entity.ShipmentsRemaining);
            entityFromDb.StartDate.Should().Be(entity.StartDate);
            entityFromDb.TotalShipments.Should().Be(entity.TotalShipments);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.CreatedByOrderItem, o => o.CreatedByOrderItemId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Subscription, o => o.SubscriptionId);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            SubscriptionInstanceEntityWrapper entityWrapper, SubscriptionInstance entity, SubscriptionInstance entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            SubscriptionInstanceEntityWrapper entityWrapper, SubscriptionInstance entity, SubscriptionInstance entityFromDb)
        {
            entityFromDb.ShipmentsRemaining.Should().Be(0);
        }

        public virtual void OnAssertExpectationsAfterDelete(SubscriptionInstanceEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ISubscriptionOrderItemDataService>().Get(entityWrapper.CreatedBySubscriptionOrderItem.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IAddressDataService>().Get(entityWrapper.ShippingAddress.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<ICustomerChargeOptionDataService>().Get(entityWrapper.CustomerChargeOption.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(SubscriptionInstanceEntityWrapper entityWrapper, SubscriptionInstance entity)
        {
            entity.ShipmentsRemaining = 0;
        }

        public virtual void ChangePropertyValuesToFailValidation(SubscriptionInstanceEntityWrapper entityWrapper, SubscriptionInstance entity)
        {
            entity.ShipmentsRemaining = -2;
        }
    }
}
