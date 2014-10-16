using System;
using System.Linq.Expressions;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Partners
{
    public partial class PartnerReferralCouponRequestEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Partner Partner { get; set; }
    }

    public partial class PartnerReferralCouponRequestTestsStrategy
    {
        public virtual void SetUpWrapper(PartnerReferralCouponRequestEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(PartnerReferralCouponRequestEntityWrapper wrapper)
        {
            wrapper.Entity =
                this.Data.CreatePartnerReferralCouponRequest(
                    this.Data.CreatePartner(
                        this.Data.CreatePartnerReferralPlan(this.Data.CreateDiscount(this.Data.CreatePromoCode())),
                        this.Data.CreateAddress(this.Data.GetExistingStateProvince()), wrapper.Visitor,
                        this.Data.CreatePromoCode()));
            wrapper.Partner = wrapper.Entity.Partner;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            PartnerReferralCouponRequestEntityWrapper entityWrapper, PartnerReferralCouponRequest entity, PartnerReferralCouponRequest entityFromDb)
        {
            entityFromDb.Quantity.Should().Be(entity.Quantity);
            entityFromDb.RequestDate.Should().Be(entity.RequestDate);
            entityFromDb.ShippedDate.Should().Be(entity.ShippedDate);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Partner, o => o.PartnerId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.PartnerReferralCouponRequestStatusType, o => o.PartnerReferralCouponRequestStatusTypeId);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            PartnerReferralCouponRequestEntityWrapper entityWrapper, PartnerReferralCouponRequest entity, PartnerReferralCouponRequest entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            PartnerReferralCouponRequestEntityWrapper entityWrapper, PartnerReferralCouponRequest entity, PartnerReferralCouponRequest entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(PartnerReferralCouponRequestEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IPartnerDataService>().Get(entityWrapper.Partner.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(PartnerReferralCouponRequestEntityWrapper entityWrapper, PartnerReferralCouponRequest entity)
        {
            entity.Quantity += entity.Quantity;
        }

        public virtual void ChangePropertyValuesToFailValidation(PartnerReferralCouponRequestEntityWrapper entityWrapper, PartnerReferralCouponRequest entity)
        {
            entity.RequestDate = DateTime.MinValue;
        }
    }
}
