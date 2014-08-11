using System;
using System.Linq.Expressions;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Subscriptions
{
    public partial class PendingSubscriptionChangeEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Customer Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public StripeCustomerChargeOption CustomerChargeOption { get; set; }
        public SubscriptionOrderItem CreatedBySubscriptionOrderItem { get; set; }
        public SubscriptionInstance CurrentSubscriptionInstance { get; set; }
        public Subscription Subscription { get; set; }
        public SubscriptionChoice SubscriptionChoiceForPendingSubscriptionChange { get; set; }
    }

    public partial class PendingSubscriptionChangeTestsStrategy
    {

        public virtual void SetUpWrapper(PendingSubscriptionChangeEntityWrapper wrapper)
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
            var subscription1 = this.Data.CreateSubscription(orderItem1, shippingAddress1, customerChargeOption1);

            //uow.Flush();
            var subscriptionInstance1 = this.Data.CreateSubscriptionInstance(subscription1, orderItem1);
            var subscriptionChoice1 = this.Data.CreateSubscriptionChoice(this.Data.CreateDisplayProduct(), this.Data.CreateSubscriptionPeriod());

            wrapper.Customer = customer1;
            wrapper.CustomerChargeOption = customerChargeOption1;
            wrapper.ShippingAddress = shippingAddress1;
            wrapper.CreatedBySubscriptionOrderItem = orderItem1;
            wrapper.Subscription = subscription1;
            wrapper.CurrentSubscriptionInstance = subscriptionInstance1;
            wrapper.SubscriptionChoiceForPendingSubscriptionChange = subscriptionChoice1;
        }

        public virtual void CreateEntity(PendingSubscriptionChangeEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreatePendingSubscriptionChange(wrapper.SubscriptionChoiceForPendingSubscriptionChange);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            PendingSubscriptionChangeEntityWrapper entityWrapper, PendingSubscriptionChange entity, PendingSubscriptionChange entityFromDb)
        {
            entityFromDb.Quantity.Should().Be(entity.Quantity);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.SubscriptionChoice, o => o.SubscriptionChoiceId);
            entityFromDb.SubscriptionChoice.Should().Be(entityWrapper.SubscriptionChoiceForPendingSubscriptionChange);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            PendingSubscriptionChangeEntityWrapper entityWrapper, PendingSubscriptionChange entity, PendingSubscriptionChange entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            PendingSubscriptionChangeEntityWrapper entityWrapper, PendingSubscriptionChange entity, PendingSubscriptionChange entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(PendingSubscriptionChangeEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ISubscriptionChoiceDataService>().Get(entityWrapper.SubscriptionChoiceForPendingSubscriptionChange.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<ISubscriptionDataService>().Get(entityWrapper.Subscription.Id).Should().NotBeNull();
            entityWrapper.CurrentSubscriptionInstance.PendingSubscriptionChange.Should().BeNull();
        }

        public virtual void ChangePropertyValues(PendingSubscriptionChangeEntityWrapper entityWrapper, PendingSubscriptionChange entity)
        {
            entity.Quantity = 90;
        }

        public virtual void ChangePropertyValuesToFailValidation(PendingSubscriptionChangeEntityWrapper entityWrapper, PendingSubscriptionChange entity)
        {
            entity.Quantity = -1;
        }
    }
}
