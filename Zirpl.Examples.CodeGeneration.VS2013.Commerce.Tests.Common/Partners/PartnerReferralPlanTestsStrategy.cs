using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Partners
{
    public partial class PartnerReferralPlanEntityWrapper
    {
        public UpdatableProperty<Discount> Discount = new UpdatableProperty<Discount>();
    }

    public partial class PartnerReferralPlanTestsStrategy
    {
        public virtual void SetUpWrapper(PartnerReferralPlanEntityWrapper wrapper)
        {
        }

        public virtual void CreateEntity(PartnerReferralPlanEntityWrapper wrapper)
        {
            wrapper.Entity = this.Data.CreatePartnerReferralPlan(this.Data.CreateDiscount(this.Data.CreatePromoCode()));
            wrapper.Discount.Original = wrapper.Entity.ReferredCustomerAwardDiscount;
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            PartnerReferralPlanEntityWrapper entityWrapper, PartnerReferralPlan entity, PartnerReferralPlan entityFromDb)
        {
            entityFromDb.Amount.Should().Be(entity.Amount);
            entityFromDb.Name.Should().Be(entity.Name);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ReferredCustomerAwardDiscount, o => o.ReferredCustomerAwardDiscountId);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            PartnerReferralPlanEntityWrapper entityWrapper, PartnerReferralPlan entity, PartnerReferralPlan entityFromDb)
        {
            entityFromDb.ReferredCustomerAwardDiscount.Id.Should().Be(entityWrapper.Discount.Original.Id);
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            PartnerReferralPlanEntityWrapper entityWrapper, PartnerReferralPlan entity, PartnerReferralPlan entityFromDb)
        {
            entityFromDb.ReferredCustomerAwardDiscount.Id.Should().Be(entityWrapper.Discount.Updated.Id);
        }

        public virtual void OnAssertExpectationsAfterDelete(
            PartnerReferralPlanEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IDiscountDataService>().Get(entityWrapper.Discount.Original.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<IDiscountDataService>().Get(entityWrapper.Discount.Updated.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(
            PartnerReferralPlanEntityWrapper entityWrapper, PartnerReferralPlan entity)
        {
            entity.Name = Guid.NewGuid().ToString();
            entity.ReferredCustomerAwardDiscount = this.Data.CreateDiscount(this.Data.CreatePromoCode());
            entityWrapper.Discount.Updated = entity.ReferredCustomerAwardDiscount;
        }

        public virtual void ChangePropertyValuesToFailValidation(PartnerReferralPlanEntityWrapper entityWrapper, PartnerReferralPlan entity)
        {
            entity.Amount = -20;
        }
    }
}
