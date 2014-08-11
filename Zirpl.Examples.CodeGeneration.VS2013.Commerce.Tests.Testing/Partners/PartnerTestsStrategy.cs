using System;
using System.Linq.Expressions;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Partners
{
    public partial class PartnerEntityWrapper : EntityWrapper<Partner>
    {
        public Visitor Visitor;
        public UpdatableProperty<Address> Address = new UpdatableProperty<Address>();
        public UpdatableProperty<PartnerReferralPlan> ReferralPlan = new UpdatableProperty<PartnerReferralPlan>();
    }

    public partial class PartnerTestsStrategy
    {
        public virtual void SetUpWrapper(PartnerEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(PartnerEntityWrapper wrapper)
        {
            wrapper.Address.Original = this.Data.CreateAddress(this.Data.GetExistingStateProvince());
            wrapper.ReferralPlan.Original = this.Data.CreatePartnerReferralPlan(this.Data.CreateDiscount(this.Data.CreatePromoCode()));

            wrapper.Entity = this.Data.CreatePartner(
                wrapper.ReferralPlan.Original,
                wrapper.Address.Original,
                wrapper.Visitor,
                this.Data.CreatePromoCode());
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            PartnerEntityWrapper entityWrapper, Partner entity, Partner entityFromDb)
        {
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Address, o => o.AddressId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ReferralPlan, o => o.ReferralPlanId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Visitor, o => o.VisitorId); 
            entityFromDb.Visitor.Id.Should().Be(entityWrapper.Visitor.Id);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            PartnerEntityWrapper entityWrapper, Partner entity, Partner entityFromDb)
        {
            entityFromDb.Address.Id.Should().Be(entityWrapper.Address.Original.Id);
            entityFromDb.ReferralPlan.Id.Should().Be(entityWrapper.ReferralPlan.Original.Id);
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            PartnerEntityWrapper entityWrapper, Partner entity, Partner entityFromDb)
        {
            entityFromDb.ReferralPlan.Id.Should().Be(entityWrapper.ReferralPlan.Updated.Id);
        }

        public virtual void OnAssertExpectationsAfterDelete(PartnerEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IAddressDataService>().Get(entityWrapper.Address.Original.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IPartnerReferralPlanDataService>().Get(entityWrapper.ReferralPlan.Original.Id).Should().NotBeNull();
            if (entityWrapper.IsUpdated)
            {
                this.DependencyResolver.Resolve<IPartnerReferralPlanDataService>().Get(entityWrapper.ReferralPlan.Updated.Id).Should().NotBeNull();
            }
        }

        public virtual void ChangePropertyValues(PartnerEntityWrapper entityWrapper, Partner entity)
        {
            entityWrapper.ReferralPlan.Updated = this.Data.CreatePartnerReferralPlan(this.Data.CreateDiscount(this.Data.CreatePromoCode()));
            entity.ReferralPlan = entityWrapper.ReferralPlan.Updated;
        }

        public virtual void ChangePropertyValuesToFailValidation(PartnerEntityWrapper entityWrapper, Partner entity)
        {
            // TODO: not a whole lot we can do here...
            entity.Visitor = null;
        }

    }
}
