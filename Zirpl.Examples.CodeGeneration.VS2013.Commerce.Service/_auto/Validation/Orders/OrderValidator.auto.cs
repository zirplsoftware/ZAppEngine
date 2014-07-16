
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class OrderValidator  : DbEntityValidatorBase<Order>
		
    {
        public OrderValidator()
        {
			this.RuleFor(o => o.Date).NotEmpty();
            this.ForeignEntityNotNullAndIdMatches(o => o.OrderChargeStatusType, o => o.OrderChargeStatusTypeId,
                OrderMetadata.OrderChargeStatusType_Name, OrderMetadata.OrderChargeStatusTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.OrderStatusType, o => o.OrderStatusTypeId,
                OrderMetadata.OrderStatusType_Name, OrderMetadata.OrderStatusTypeId_Name);
			this.RuleFor(o => o.SubtotalAmountBeforeDiscount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(OrderMetadata.SubtotalAmountBeforeDiscount_MinValue, OrderMetadata.SubtotalAmountBeforeDiscount_MaxValue);
			this.RuleFor(o => o.OriginalSubtotalAmount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(OrderMetadata.OriginalSubtotalAmount_MinValue, OrderMetadata.OriginalSubtotalAmount_MaxValue);
			this.RuleFor(o => o.OriginalTaxAmount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(OrderMetadata.OriginalTaxAmount_MinValue, OrderMetadata.OriginalTaxAmount_MaxValue);
			this.RuleFor(o => o.OriginalTotalAmount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(OrderMetadata.OriginalTotalAmount_MinValue, OrderMetadata.OriginalTotalAmount_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ShippingAddress, o => o.ShippingAddressId,
                OrderMetadata.ShippingAddress_Name, OrderMetadata.ShippingAddressId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Customer, o => o.CustomerId,
                OrderMetadata.Customer_Name, OrderMetadata.CustomerId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.CustomerChargeOption, o => o.CustomerChargeOptionId,
                OrderMetadata.CustomerChargeOption_Name, OrderMetadata.CustomerChargeOptionId_Name);
			// unsure how to follow this for validation or even if it should with EF- Collection property: Charges
			// unsure how to follow this for validation or even if it should with EF- Collection property: DiscountUsages
			// unsure how to follow this for validation or even if it should with EF- Collection property: OrderItems

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

