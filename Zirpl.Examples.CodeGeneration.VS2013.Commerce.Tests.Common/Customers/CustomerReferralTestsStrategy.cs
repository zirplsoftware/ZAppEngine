using System;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Customers
{
    public partial class CustomerReferralEntityWrapper : EntityWrapper<CustomerReferral>
    {
        public Visitor ReferringCustomerVisitor { get; set; }
        public Customer ReferringCustomer { get; set; }
        public Visitor ReferredCustomerVisitor { get; set; }
        public Customer ReferredCustomer { get; set; }
        public PromoCode PromoCode { get; set; }
    }

    public partial class CustomerReferralTestsStrategy
    {
        public virtual void SetUpWrapper(CustomerReferralEntityWrapper wrapper)
        {
            wrapper.ReferringCustomerVisitor = this.Data.GenerateVisitorWithUser();
            wrapper.ReferredCustomerVisitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(CustomerReferralEntityWrapper wrapper)
        {
            wrapper.PromoCode = this.Data.CreatePromoCode();
            wrapper.ReferringCustomer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.ReferringCustomerVisitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Entity = this.Data.CreateCustomerReferral(wrapper.ReferringCustomer, wrapper.PromoCode);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            CustomerReferralEntityWrapper entityWrapper, CustomerReferral entity, CustomerReferral entityFromDb)
        {
            entityFromDb.ReferredCustomerJoinedDate.Should().Be(entity.ReferredCustomerJoinedDate);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ReferringCustomer, o => o.ReferringCustomerId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.PromoCode, o => o.PromoCodeId);
            entityFromDb.PromoCode.Should().Be(entityWrapper.PromoCode);
            entityFromDb.ReferringCustomer.Should().Be(entityWrapper.ReferringCustomer);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            CustomerReferralEntityWrapper entityWrapper, CustomerReferral entity, CustomerReferral entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            CustomerReferralEntityWrapper entityWrapper, CustomerReferral entity, CustomerReferral entityFromDb)
        {
            entityFromDb.ReferredCustomerJoinedDate.Should().HaveValue();
            entityFromDb.ReferredCustomerJoinedDate.Should().Be(entity.ReferredCustomerJoinedDate);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ReferredCustomer, o => o.ReferredCustomerId);
            entityFromDb.ReferredCustomer.Should().Be(entityWrapper.ReferredCustomer);
        }

        public virtual void OnAssertExpectationsAfterDelete(CustomerReferralEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.ReferringCustomer.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IPromoCodeDataService>().Get(entityWrapper.PromoCode.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.ReferredCustomer.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(CustomerReferralEntityWrapper entityWrapper, CustomerReferral entity)
        {
            entityWrapper.ReferredCustomer = this.Data.CreateCustomer(this.Data.CreatePromoCode(),
                                                                      visitor: entityWrapper.ReferredCustomerVisitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            entity.ReferredCustomerJoinedDate = DateTime.Now;
            entity.ReferredCustomer = entityWrapper.ReferredCustomer;
        }

        public virtual void ChangePropertyValuesToFailValidation(CustomerReferralEntityWrapper entityWrapper, CustomerReferral entity)
        {
            if (!entity.IsPersisted)
            {
                entity.PromoCode = null;
            }
            else
            {
                entity.ReferredCustomerJoinedDate = null; // should trigger since it was set above    
            }
        }
    }
}
