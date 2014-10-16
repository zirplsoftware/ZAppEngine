using System;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Customers
{
    public partial class CustomerChargeOptionEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Customer Customer { get; set; }
    }

    public partial class CustomerChargeOptionTestsStrategy
    {
        public virtual void SetUpWrapper(CustomerChargeOptionEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }
        public virtual void CreateEntity(CustomerChargeOptionEntityWrapper wrapper)
        {
            wrapper.Customer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.Visitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Entity = this.Data.CreateStripeCustomerChargeOption(wrapper.Customer, this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            CustomerChargeOptionEntityWrapper entityWrapper, CustomerChargeOption entity, CustomerChargeOption entityFromDb)
        {
            var stripeEntity = (StripeCustomerChargeOption)entity;
            var stripeEntityFromDb = (StripeCustomerChargeOption)entityFromDb;
            stripeEntityFromDb.StripeCustomerId.Should().Be(stripeEntity.StripeCustomerId);
            stripeEntityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(stripeEntity, o => o.Customer, o => o.CustomerId);
            entityFromDb.Customer.Should().Be(entityWrapper.Customer);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            CustomerChargeOptionEntityWrapper entityWrapper, CustomerChargeOption entity, CustomerChargeOption entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            CustomerChargeOptionEntityWrapper entityWrapper, CustomerChargeOption entity, CustomerChargeOption entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(CustomerChargeOptionEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.Customer.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(CustomerChargeOptionEntityWrapper entityWrapper, CustomerChargeOption entity)
        {
            var stripeEntity = (StripeCustomerChargeOption)entity;
            stripeEntity.StripeCustomerId = Guid.NewGuid().ToString();
        }

        public virtual void ChangePropertyValuesToFailValidation(CustomerChargeOptionEntityWrapper entityWrapper, CustomerChargeOption entity)
        {
            var stripeEntity = (StripeCustomerChargeOption)entity;
            stripeEntity.StripeCustomerId = null;
        }
    }
}
