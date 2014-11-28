
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class OrderValidator  : DbEntityValidatorBase<Order>
		
    {
        public OrderValidator()
        {
			this.RuleFor(o => o.Date).NotEmpty();
            this.ForeignEntityNotNullAndIdMatches(o => o.OrderChargeStatusType, o => o.OrderChargeStatusTypeId,
                OrderMetadataConstants.OrderChargeStatusType_Name, OrderMetadataConstants.OrderChargeStatusTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.OrderStatusType, o => o.OrderStatusTypeId,
                OrderMetadataConstants.OrderStatusType_Name, OrderMetadataConstants.OrderStatusTypeId_Name);
			this.RuleFor(o => o.SubtotalAmountBeforeDiscount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(OrderMetadataConstants.SubtotalAmountBeforeDiscount_MinValue, OrderMetadataConstants.SubtotalAmountBeforeDiscount_MaxValue);
			this.RuleFor(o => o.OriginalSubtotalAmount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(OrderMetadataConstants.OriginalSubtotalAmount_MinValue, OrderMetadataConstants.OriginalSubtotalAmount_MaxValue);
			this.RuleFor(o => o.OriginalTaxAmount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(OrderMetadataConstants.OriginalTaxAmount_MinValue, OrderMetadataConstants.OriginalTaxAmount_MaxValue);
			this.RuleFor(o => o.OriginalTotalAmount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(OrderMetadataConstants.OriginalTotalAmount_MinValue, OrderMetadataConstants.OriginalTotalAmount_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ShippingAddress, o => o.ShippingAddressId,
                OrderMetadataConstants.ShippingAddress_Name, OrderMetadataConstants.ShippingAddressId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Customer, o => o.CustomerId,
                OrderMetadataConstants.Customer_Name, OrderMetadataConstants.CustomerId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.CustomerChargeOption, o => o.CustomerChargeOptionId,
                OrderMetadataConstants.CustomerChargeOption_Name, OrderMetadataConstants.CustomerChargeOptionId_Name);
			// unsure how to follow this for validation or even if it should with EF- Collection property: Charges
			// unsure how to follow this for validation or even if it should with EF- Collection property: DiscountUsages
			// unsure how to follow this for validation or even if it should with EF- Collection property: OrderItems

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

