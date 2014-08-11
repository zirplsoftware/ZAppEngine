using System;
using System.Linq.Expressions;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Partners
{
    public partial class PartnerReferralEntityWrapper
    {
        public Visitor PartnerVisitor { get; set; }
        public Partner Partner { get; set; }
        public Visitor ReferredCustomerVisitor { get; set; }
        public Customer ReferredCustomer { get; set; }
        public PromoCode PromoCode { get; set; }
        public PartnerReferralCouponRequest PartnerReferralCouponRequest { get; set; }
        public PartnerReferralPlan PartnerReferralPlan { get; set; }
    }

    public partial class PartnerReferralTestsStrategy
    {
        public virtual void SetUpWrapper(PartnerReferralEntityWrapper wrapper)
        {
            wrapper.PartnerVisitor = this.Data.GenerateVisitorWithUser();
            wrapper.ReferredCustomerVisitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(PartnerReferralEntityWrapper wrapper)
        {
            wrapper.PromoCode = this.Data.CreatePromoCode();
            wrapper.Partner = this.Data.CreatePartner(
                this.Data.CreatePartnerReferralPlan(this.Data.CreateDiscount(this.Data.CreatePromoCode())),
                this.Data.CreateAddress(this.Data.GetExistingStateProvince()),
                wrapper.PartnerVisitor,
                this.Data.CreatePromoCode());
            wrapper.PartnerReferralCouponRequest = this.Data.CreatePartnerReferralCouponRequest(wrapper.Partner);

            wrapper.Entity = this.Data.CreatePartnerReferral(wrapper.Partner, wrapper.PartnerReferralCouponRequest, wrapper.PromoCode);
            wrapper.Entity.Partner = wrapper.Partner;
            wrapper.Entity.PromoCode = wrapper.PromoCode;
            wrapper.Entity.Request = wrapper.PartnerReferralCouponRequest;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            PartnerReferralEntityWrapper entityWrapper, PartnerReferral entity, PartnerReferral entityFromDb)
        {
            entityFromDb.ReferredCustomerJoinedDate.Should().Be(entity.ReferredCustomerJoinedDate);
            AssertionUtils.AssertNavigationPropertyMatchesAndIsNotNull(entity, entityFromDb, o => o.Partner, o => o.PartnerId);
            AssertionUtils.AssertNavigationPropertyMatchesAndIsNotNull(entity, entityFromDb, o => o.PromoCode, o => o.PromoCodeId);
            AssertionUtils.AssertNavigationPropertyMatchesAndIsNotNull(entity, entityFromDb, o => o.Request, o => o.RequestId);
            entityFromDb.PromoCode.Should().Be(entityWrapper.PromoCode);
            entityFromDb.Partner.Should().Be(entityWrapper.Partner);
            entityFromDb.Request.Should().Be(entityWrapper.PartnerReferralCouponRequest);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            PartnerReferralEntityWrapper entityWrapper, PartnerReferral entity, PartnerReferral entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            PartnerReferralEntityWrapper entityWrapper, PartnerReferral entity, PartnerReferral entityFromDb)
        {
            entityFromDb.ReferredCustomerJoinedDate.Should().HaveValue();
            entityFromDb.ReferredCustomerJoinedDate.Should().Be(entity.ReferredCustomerJoinedDate);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ReferredCustomer, o => o.ReferredCustomerId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Plan, o => o.PlanId);
            entityFromDb.ReferredCustomer.Should().Be(entityWrapper.ReferredCustomer);
            entityFromDb.Plan.Should().Be(entityWrapper.PartnerReferralPlan);
        }

        public virtual void OnAssertExpectationsAfterDelete(PartnerReferralEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IPartnerDataService>().Get(entityWrapper.Partner.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IPromoCodeDataService>().Get(entityWrapper.PromoCode.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IPartnerReferralCouponRequestDataService>().Get(entityWrapper.PartnerReferralCouponRequest.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.ReferredCustomer.Id).Should().NotBeNull();
                this.DependencyResolver.Resolve<IPartnerReferralPlanDataService>().Get(entityWrapper.PartnerReferralPlan.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(PartnerReferralEntityWrapper entityWrapper, PartnerReferral entity)
        {
            entityWrapper.ReferredCustomer = this.Data.CreateCustomer(this.Data.CreatePromoCode(),
                                                                      visitor: entityWrapper.ReferredCustomerVisitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            entityWrapper.PartnerReferralPlan = entityWrapper.Partner.ReferralPlan;

            entity.ReferredCustomerJoinedDate = DateTime.Now;
            entity.ReferredCustomer = entityWrapper.ReferredCustomer;
            entity.Plan = entityWrapper.PartnerReferralPlan;
        }

        public virtual void ChangePropertyValuesToFailValidation(PartnerReferralEntityWrapper entityWrapper, PartnerReferral entity)
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
