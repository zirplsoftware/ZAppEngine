using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class DiscountUsageMetadataConstants : MetadataConstantsBase
    {
        public const string DateUsed_Name = "DateUsed";
		public const bool DateUsed_IsRequired = true;

        public const string Discount_Name = "Discount";
		public const bool Discount_IsRequired = true;

		public const string DiscountId_Name = "DiscountId";
		public const bool DiscountId_IsRequired = true;

        public const string Order_Name = "Order";
		public const bool Order_IsRequired = true;

		public const string OrderId_Name = "OrderId";
		public const bool OrderId_IsRequired = true;

        public const string OrderItem_Name = "OrderItem";
		public const bool OrderItem_IsRequired = false;

		public const string OrderItemId_Name = "OrderItemId";
		public const bool OrderItemId_IsRequired = false;

	}
}
