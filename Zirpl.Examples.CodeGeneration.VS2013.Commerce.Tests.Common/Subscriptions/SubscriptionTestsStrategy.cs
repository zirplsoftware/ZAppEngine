using System;
using System.Linq.Expressions;
using FluentAssertions;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Subscriptions
{
    public partial class SubscriptionEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Customer Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public StripeCustomerChargeOption CustomerChargeOption { get; set; }
        public SubscriptionOrderItem CreatedBySubscriptionOrderItem { get; set; }
        public SubscriptionInstance CurrentSubscriptionInstance { get; set; }
    }

    public partial class SubscriptionTestsStrategy
    {
        public virtual void SetUpWrapper(SubscriptionEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();

            // this is necessary because:

            // this fails because a single SubscriptionInitialOrderItem is created without being saved
            // and then it's own ResultingSubscriptionInstance is created
            // this means that 0 is the common key between the 2, which is a 1 to 0..1 relationship
            // 
            // with this List method, a 2nd SubscriptionInitialOrderItem is then created, which fails
            // because it violates the constraint.

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

        public virtual void CreateEntity(SubscriptionEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreateSubscription(wrapper.CreatedBySubscriptionOrderItem, wrapper.ShippingAddress, wrapper.CustomerChargeOption);
            wrapper.CurrentSubscriptionInstance = this.Data.CreateSubscriptionInstance(wrapper.Entity, wrapper.CreatedBySubscriptionOrderItem);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            SubscriptionEntityWrapper entityWrapper, Subscription entity, Subscription entityFromDb)
        {
            entityFromDb.NextChargeDate.Should().Be(entity.NextChargeDate);
            entityFromDb.NextShipmentDate.Should().Be(entity.NextShipmentDate);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.StatusType, o => o.StatusTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ShippingAddress, o => o.ShippingAddressId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.CustomerChargeOption, o => o.CustomerChargeOptionId);
            entityFromDb.ShippingAddress.Should().Be(entityWrapper.ShippingAddress);
            entityFromDb.CustomerChargeOption.Should().Be(entityWrapper.CustomerChargeOption);
            entityFromDb.StatusType.Should().Be(entityWrapper.Entity.StatusType);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            SubscriptionEntityWrapper entityWrapper, Subscription entity, Subscription entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            SubscriptionEntityWrapper entityWrapper, Subscription entity, Subscription entityFromDb)
        {
            entityFromDb.StatusType.EnumValue.Should().Be(SubscriptionStatusTypeEnum.RenewalFailed);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.CurrentSubscriptionInstance, o => o.CurrentSubscriptionInstanceId);
            entityFromDb.CurrentSubscriptionInstance.Should().Be(entityWrapper.Entity.CurrentSubscriptionInstance);
        }

        public virtual void OnAssertExpectationsAfterDelete(SubscriptionEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ISubscriptionOrderItemDataService>().Get(entityWrapper.CreatedBySubscriptionOrderItem.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IAddressDataService>().Get(entityWrapper.ShippingAddress.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<ICustomerChargeOptionDataService>().Get(entityWrapper.CustomerChargeOption.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<ISubscriptionInstanceDataService>().Get(entityWrapper.CurrentSubscriptionInstance.Id).Should().BeNull();
        }

        public virtual void ChangePropertyValues(SubscriptionEntityWrapper entityWrapper, Subscription entity)
        {
            entity.StatusType = this.DependencyResolver.Resolve<ISubscriptionStatusTypeDataService>().Get((byte)SubscriptionStatusTypeEnum.RenewalFailed);
            entity.CurrentSubscriptionInstance = entityWrapper.CurrentSubscriptionInstance;
        }

        public virtual void ChangePropertyValuesToFailValidation(SubscriptionEntityWrapper entityWrapper, Subscription entity)
        {
            entity.NextChargeDate = DateTime.MinValue;
        }
    }
}
