

using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Orders
{
    public partial class StripeChargeEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Order Order { get; set; }
    }

    public partial class StripeChargeTestsStrategy
    {
        public virtual void SetUpWrapper(StripeChargeEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(StripeChargeEntityWrapper wrapper)
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
            StripeChargeEntityWrapper entityWrapper, StripeCharge entity, StripeCharge entityFromDb)
        {
            entityFromDb.Date.Should().Be(entity.Date);
            entityFromDb.Amount.Should().Be(entity.Amount);
            entityFromDb.StripeChargeId.Should().Be(entityFromDb.StripeChargeId);
            entityFromDb.StripeFee.Should().Be(entityFromDb.StripeFee);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ChargeMethodType, o => o.ChargeMethodTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ChargeStatusType, o => o.ChargeStatusTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ChargeType, o => o.ChargeTypeId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Order, o => o.OrderId);
            entityFromDb.Order.Should().Be(entityWrapper.Order);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            StripeChargeEntityWrapper entityWrapper, StripeCharge entity, StripeCharge entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            StripeChargeEntityWrapper entityWrapper, StripeCharge entity, StripeCharge entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(StripeChargeEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IOrderDataService>().Get(entityWrapper.Order.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(StripeChargeEntityWrapper entityWrapper, StripeCharge entity)
        {
        }

        public virtual void ChangePropertyValuesToFailValidation(StripeChargeEntityWrapper entityWrapper, StripeCharge entity)
        {
            entity.StripeChargeId = null;
        }
    }
}
