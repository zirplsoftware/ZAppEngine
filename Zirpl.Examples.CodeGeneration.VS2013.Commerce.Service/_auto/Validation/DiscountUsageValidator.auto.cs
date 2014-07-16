
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class DiscountUsageValidator  : DbEntityValidatorBase<DiscountUsage>
		
    {
        public DiscountUsageValidator()
        {
			this.RuleFor(o => o.DateUsed).NotEmpty();
            this.ForeignEntityNotNullAndIdMatches(o => o.Discount, o => o.DiscountId,
                DiscountUsageMetadata.Discount_Name, DiscountUsageMetadata.DiscountId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Order, o => o.OrderId,
                DiscountUsageMetadata.Order_Name, DiscountUsageMetadata.OrderId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.OrderItem, o => o.OrderItemId,
                DiscountUsageMetadata.OrderItem_Name, DiscountUsageMetadata.OrderItemId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

