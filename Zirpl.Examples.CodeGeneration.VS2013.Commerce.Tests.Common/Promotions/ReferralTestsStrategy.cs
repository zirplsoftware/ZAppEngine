using System;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Promotions
{
    public partial class ReferralEntityWrapper
    {
        public Visitor ReferringCustomerVisitor { get; set; }
        public Customer ReferringCustomer { get; set; }
        public Visitor ReferredCustomerVisitor { get; set; }
        public Customer ReferredCustomer { get; set; }
        public PromoCode PromoCode { get; set; }
    }

    public partial class ReferralTestsStrategy
    {
        public virtual void SetUpWrapper(ReferralEntityWrapper wrapper)
        {
            wrapper.ReferringCustomerVisitor = this.Data.GenerateVisitorWithUser();
            wrapper.ReferredCustomerVisitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(ReferralEntityWrapper wrapper)
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
            ReferralEntityWrapper entityWrapper, Referral entity, Referral entityFromDb)
        {
            var customerReferralEntity = (CustomerReferral)entity;
            var customerReferralEntityFromDb = (CustomerReferral)entityFromDb;
            customerReferralEntityFromDb.ReferredCustomerJoinedDate.Should().Be(customerReferralEntity.ReferredCustomerJoinedDate);
            customerReferralEntityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(customerReferralEntity, o => ((CustomerReferral)o).ReferringCustomer, o => ((CustomerReferral)o).ReferringCustomerId);
            customerReferralEntityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(customerReferralEntity, o => o.PromoCode, o => o.PromoCodeId);
            customerReferralEntityFromDb.PromoCode.Should().Be(entityWrapper.PromoCode);
            customerReferralEntityFromDb.ReferringCustomer.Should().Be(entityWrapper.ReferringCustomer);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            ReferralEntityWrapper entityWrapper, Referral entity, Referral entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            ReferralEntityWrapper entityWrapper, Referral entity, Referral entityFromDb)
        {
            entityFromDb.ReferredCustomerJoinedDate.Should().HaveValue();
            entityFromDb.ReferredCustomerJoinedDate.Should().Be(entity.ReferredCustomerJoinedDate);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ReferredCustomer, o => o.ReferredCustomerId);
            entityFromDb.ReferredCustomer.Should().Be(entityWrapper.ReferredCustomer);
        }

        public virtual void OnAssertExpectationsAfterDelete(ReferralEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.ReferringCustomer.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IPromoCodeDataService>().Get(entityWrapper.PromoCode.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.ReferredCustomer.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(ReferralEntityWrapper entityWrapper, Referral entity)
        {
            entityWrapper.ReferredCustomer = this.Data.CreateCustomer(this.Data.CreatePromoCode(),
                                                                      visitor: entityWrapper.ReferredCustomerVisitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            var customerReferralEntity = (CustomerReferral)entity;
            customerReferralEntity.ReferredCustomerJoinedDate = DateTime.Now;
            customerReferralEntity.ReferredCustomer = entityWrapper.ReferredCustomer;
        }

        public virtual void ChangePropertyValuesToFailValidation(ReferralEntityWrapper entityWrapper, Referral entity)
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
