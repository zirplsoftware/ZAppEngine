using System;
using FluentAssertions;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Service;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Orders
{
    public partial class OrderEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Customer Customer { get; set; }
        public Address ShippingAddress { get; set; }
        public StripeCustomerChargeOption CustomerChargeOption { get; set; }
    }

    public partial class OrderTestsStrategy
    {
        public virtual void SetUpWrapper(OrderEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(OrderEntityWrapper wrapper)
        {
            wrapper.Customer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.Visitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.ShippingAddress = wrapper.Customer.CurrentShippingAddress;
            wrapper.CustomerChargeOption = this.Data.CreateStripeCustomerChargeOption(wrapper.Customer, this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Entity = this.Data.CreateOrder(wrapper.Customer, wrapper.CustomerChargeOption);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            OrderEntityWrapper entityWrapper, Order entity, Order entityFromDb)
        {
            entityFromDb.Date.Should().Be(entity.Date);
            //entityFromDb.ShippingAmountBeforeDiscount.Should().Be(entity.ShippingAmountBeforeDiscount);
            entityFromDb.SubtotalAmountBeforeDiscount.Should().Be(entity.SubtotalAmountBeforeDiscount);
            //entityFromDb.OriginalShippingAmount.Should().Be(entity.OriginalShippingAmount);
            entityFromDb.OriginalSubtotalAmount.Should().Be(entity.OriginalSubtotalAmount);
            entityFromDb.OriginalTaxAmount.Should().Be(entity.OriginalTaxAmount);
            entityFromDb.OriginalTotalAmount.Should().Be(entity.OriginalTotalAmount);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Customer, o => o.CustomerId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.ShippingAddress, o => o.ShippingAddressId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.CustomerChargeOption, o => o.CustomerChargeOptionId);
            entityFromDb.Customer.Should().Be(entityWrapper.Customer);
            entityFromDb.ShippingAddress.Should().Be(entityWrapper.ShippingAddress);
            entityFromDb.CustomerChargeOption.Should().Be(entityWrapper.CustomerChargeOption);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            OrderEntityWrapper entityWrapper, Order entity, Order entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            OrderEntityWrapper entityWrapper, Order entity, Order entityFromDb)
        {
            entityFromDb.OrderChargeStatusType.GetEnumValue().Should().Be(OrderChargeStatusTypeEnum.Failed);
            entityFromDb.OrderStatusType.GetEnumValue().Should().Be(OrderStatusTypeEnum.PaymentFailed);
        }

        public virtual void OnAssertExpectationsAfterDelete(OrderEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<ICustomerDataService>().Get(entityWrapper.Customer.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IAddressDataService>().Get(entityWrapper.ShippingAddress.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<ICustomerChargeOptionDataService>().Get(entityWrapper.CustomerChargeOption.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(OrderEntityWrapper entityWrapper, Order entity)
        {
            entity.OrderChargeStatusType = this.DependencyResolver.Resolve<ISupportsGetById<OrderChargeStatusType, byte>>().Get((byte)OrderChargeStatusTypeEnum.Failed);
            entity.OrderStatusType = this.DependencyResolver.Resolve<ISupportsGetById<OrderStatusType, byte>>().Get((byte)OrderStatusTypeEnum.PaymentFailed);
        }

        public virtual void ChangePropertyValuesToFailValidation(OrderEntityWrapper entityWrapper, Order entity)
        {
            entity.Date = DateTime.MinValue;
        }
    }
}
