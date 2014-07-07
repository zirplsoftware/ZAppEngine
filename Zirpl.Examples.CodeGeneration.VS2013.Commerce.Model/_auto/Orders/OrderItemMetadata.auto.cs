using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class OrderItemMetadata : MetadataBase
    {
        public const string Quantity_Name = "Quantity";
		public const bool Quantity_IsRequired = true;
		public const int Quantity_MinValue = 1;
        public const int Quantity_MaxValue = int.MaxValue;

        public const string ItemName_Name = "ItemName";
		public const bool ItemName_IsRequired = true;
		public const bool ItemName_IsMaxLength = false;
        public const int ItemName_MinLength = 1;
		public const int ItemName_MaxLength = 512;

        public const string ItemAmountBeforeDiscount_Name = "ItemAmountBeforeDiscount";
		public const bool ItemAmountBeforeDiscount_IsRequired = true;
		public const decimal ItemAmountBeforeDiscount_MinValue = 0m;
        public const decimal ItemAmountBeforeDiscount_MaxValue = decimal.MaxValue;
		public const double ItemAmountBeforeDiscount_MinValue_Double = 0D;
        public const double ItemAmountBeforeDiscount_MaxValue_Double = double.MaxValue;

        public const string OriginalItemAmount_Name = "OriginalItemAmount";
		public const bool OriginalItemAmount_IsRequired = true;
		public const decimal OriginalItemAmount_MinValue = 0m;
        public const decimal OriginalItemAmount_MaxValue = decimal.MaxValue;
		public const double OriginalItemAmount_MinValue_Double = 0D;
        public const double OriginalItemAmount_MaxValue_Double = double.MaxValue;

        public const string Cancelled_Name = "Cancelled";
		public const bool Cancelled_IsRequired = true;

        public const string Order_Name = "Order";
		public const bool Order_IsRequired = true;

		public const string OrderId_Name = "OrderId";
		public const bool OrderId_IsRequired = true;

        public const string DisplayProduct_Name = "DisplayProduct";
		public const bool DisplayProduct_IsRequired = true;

		public const string DisplayProductId_Name = "DisplayProductId";
		public const bool DisplayProductId_IsRequired = true;

        public const string DiscountUsages_Name = "DiscountUsages";
		public const bool DiscountUsages_IsRequired = false;

	}
}
