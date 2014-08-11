using System;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Orders
{
    public partial class StripeCustomerChargeOptionEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Customer Customer { get; set; }
    }

    public partial class StripeCustomerChargeOptionTestsStrategy
    {
        public virtual void SetUpWrapper(StripeCustomerChargeOptionEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(StripeCustomerChargeOptionEntityWrapper wrapper)
        {
            wrapper.Customer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.Visitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Entity = this.Data.CreateStripeCustomerChargeOption(wrapper.Customer, this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            StripeCustomerChargeOptionEntityWrapper entityWrapper, StripeCustomerChargeOption entity, StripeCustomerChargeOption entityFromDb)
        {
            entityFromDb.StripeCustomerId.Should().Be(entity.StripeCustomerId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Customer, o => o.CustomerId);
            entityFromDb.Customer.Should().Be(entityWrapper.Customer);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            StripeCustomerChargeOptionEntityWrapper entityWrapper, StripeCustomerChargeOption entity, StripeCustomerChargeOption entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            StripeCustomerChargeOptionEntityWrapper entityWrapper, StripeCustomerChargeOption entity, StripeCustomerChargeOption entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(StripeCustomerChargeOptionEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.Customer.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(StripeCustomerChargeOptionEntityWrapper entityWrapper, StripeCustomerChargeOption entity)
        {
            entity.StripeCustomerId = Guid.NewGuid().ToString();
        }

        public virtual void ChangePropertyValuesToFailValidation(StripeCustomerChargeOptionEntityWrapper entityWrapper, StripeCustomerChargeOption entity)
        {
            entity.StripeCustomerId = null;
        }
    }
}
