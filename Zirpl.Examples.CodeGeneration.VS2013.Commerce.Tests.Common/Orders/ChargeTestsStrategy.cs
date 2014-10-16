using System;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Orders
{
    public partial class ChargeEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Order Order { get; set; }
    }

    public partial class ChargeTestsStrategy
    {
        public virtual void SetUpWrapper(ChargeEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(ChargeEntityWrapper wrapper)
        {
            var customer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.Visitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Order = this.Data.CreateOrder(customer, this.Data.CreateStripeCustomerChargeOption(customer, this.Data.CreateAddress(this.Data.GetExistingStateProvince())));
            wrapper.Entity = this.Data.CreateStripeCharge(wrapper.Order);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            ChargeEntityWrapper entityWrapper, Charge entity, Charge entityFromDb)
        {
            entityFromDb.Date.Should().Be(entity.Date);
            entityFromDb.Amount.Should().Be(entity.Amount);

            var stripeEntity = (StripeCharge) entity;
            var stripeEntityFromDb = (StripeCharge) entityFromDb;
            stripeEntityFromDb.StripeChargeId.Should().Be(stripeEntity.StripeChargeId);
            stripeEntityFromDb.StripeFee.Should().Be(stripeEntity.StripeFee);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ChargeMethodType, o => o.ChargeMethodTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ChargeStatusType, o => o.ChargeStatusTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ChargeType, o => o.ChargeTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Order, o => o.OrderId);
            entityFromDb.Order.Should().Be(entityWrapper.Order);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            ChargeEntityWrapper entityWrapper, Charge entity, Charge entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            ChargeEntityWrapper entityWrapper, Charge entity, Charge entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(ChargeEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IOrderDataService>().Get(entityWrapper.Order.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(ChargeEntityWrapper entityWrapper, Charge entity)
        {
        }

        public virtual void ChangePropertyValuesToFailValidation(ChargeEntityWrapper entityWrapper, Charge entity)
        {
            entity.Date = DateTime.MinValue;
        }
    }
}
