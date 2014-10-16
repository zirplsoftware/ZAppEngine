using System;
using FluentAssertions;
using Zirpl.AppEngine.Testing;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Tests.Orders
{
    public partial class OrderItemEntityWrapper
    {
        public Visitor Visitor { get; set; }
        public Order Order { get; set; }
        public DisplayProduct DisplayProduct { get; set; }
    }

    public partial class OrderItemTestsStrategy
    {
        public virtual void SetUpWrapper(OrderItemEntityWrapper wrapper)
        {
            wrapper.Visitor = this.Data.GenerateVisitorWithUser();
        }

        public virtual void CreateEntity(OrderItemEntityWrapper wrapper)
        {
            var customer = this.Data.CreateCustomer(this.Data.CreatePromoCode(), visitor: wrapper.Visitor, shippingAddress: this.Data.CreateAddress(this.Data.GetExistingStateProvince()));
            wrapper.Order = this.Data.CreateOrder(customer, this.Data.CreateStripeCustomerChargeOption(customer, this.Data.CreateAddress(this.Data.GetExistingStateProvince())));
            wrapper.DisplayProduct = this.Data.CreateDisplayProduct();
            wrapper.Entity = this.Data.CreateOrderItem(wrapper.Order, wrapper.DisplayProduct);
        }

        public virtual int IncrementId(int id)
        {
            return ++id;
        }

        public virtual void OnAssertCommonPersistedEntityExpectations(
            OrderItemEntityWrapper entityWrapper, OrderItem entity, OrderItem entityFromDb)
        {
            entityFromDb.Quantity.Should().Be(entity.Quantity);
            //entityFromDb.ShippingAmountBeforeDiscount.Should().Be(entity.ShippingAmountBeforeDiscount);
            entityFromDb.ItemAmountBeforeDiscount.Should().Be(entity.ItemAmountBeforeDiscount);
            //entityFromDb.OriginalShippingAmount.Should().Be(entity.OriginalShippingAmount);
            entityFromDb.OriginalItemAmount.Should().Be(entity.OriginalItemAmount);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.DisplayProduct, o => o.DisplayProductId);
            entityFromDb.AssertNavigationPropertyMatchesAndIsNotNull(entity, o => o.Order, o => o.OrderId);
            entityFromDb.DisplayProduct.Should().Be(entityWrapper.DisplayProduct);
            entityFromDb.Order.Should().Be(entityWrapper.Order);
        }

        public virtual void OnAssertExpectationsAfterInsert(
            OrderItemEntityWrapper entityWrapper, OrderItem entity, OrderItem entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterUpdate(
            OrderItemEntityWrapper entityWrapper, OrderItem entity, OrderItem entityFromDb)
        {
        }

        public virtual void OnAssertExpectationsAfterDelete(OrderItemEntityWrapper entityWrapper)
        {
            this.DependencyResolver.Resolve<IDisplayProductDataService>().Get(entityWrapper.DisplayProduct.Id).Should().NotBeNull();
            this.DependencyResolver.Resolve<IOrderDataService>().Get(entityWrapper.Order.Id).Should().NotBeNull();
        }

        public virtual void ChangePropertyValues(OrderItemEntityWrapper entityWrapper, OrderItem entity)
        {
            entity.UpdatedDate = DateTime.Now;
        }

        public virtual void ChangePropertyValuesToFailValidation(OrderItemEntityWrapper entityWrapper, OrderItem entity)
        {
            entity.Quantity = -1;
        }
    }
}
