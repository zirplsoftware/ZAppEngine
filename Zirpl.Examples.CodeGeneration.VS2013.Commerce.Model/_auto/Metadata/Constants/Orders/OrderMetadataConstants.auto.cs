using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders
{
    public partial class OrderMetadataConstants : MetadataConstantsBase
    {
        public const string Date_Name = "Date";
		public const bool Date_IsRequired = true;

        public const string OrderChargeStatusType_Name = "OrderChargeStatusType";
		public const bool OrderChargeStatusType_IsRequired = true;

		public const string OrderChargeStatusTypeId_Name = "OrderChargeStatusTypeId";
		public const bool OrderChargeStatusTypeId_IsRequired = true;

        public const string OrderStatusType_Name = "OrderStatusType";
		public const bool OrderStatusType_IsRequired = true;

		public const string OrderStatusTypeId_Name = "OrderStatusTypeId";
		public const bool OrderStatusTypeId_IsRequired = true;

        public const string SubtotalAmountBeforeDiscount_Name = "SubtotalAmountBeforeDiscount";
		public const bool SubtotalAmountBeforeDiscount_IsRequired = true;
		public const decimal SubtotalAmountBeforeDiscount_MinValue = 0m;
        public const decimal SubtotalAmountBeforeDiscount_MaxValue = decimal.MaxValue;
		public const double SubtotalAmountBeforeDiscount_MinValue_Double = 0D;
        public const double SubtotalAmountBeforeDiscount_MaxValue_Double = double.MaxValue;

        public const string OriginalSubtotalAmount_Name = "OriginalSubtotalAmount";
		public const bool OriginalSubtotalAmount_IsRequired = true;
		public const decimal OriginalSubtotalAmount_MinValue = 0m;
        public const decimal OriginalSubtotalAmount_MaxValue = decimal.MaxValue;
		public const double OriginalSubtotalAmount_MinValue_Double = 0D;
        public const double OriginalSubtotalAmount_MaxValue_Double = double.MaxValue;

        public const string OriginalTaxAmount_Name = "OriginalTaxAmount";
		public const bool OriginalTaxAmount_IsRequired = true;
		public const decimal OriginalTaxAmount_MinValue = 0m;
        public const decimal OriginalTaxAmount_MaxValue = decimal.MaxValue;
		public const double OriginalTaxAmount_MinValue_Double = 0D;
        public const double OriginalTaxAmount_MaxValue_Double = double.MaxValue;

        public const string OriginalTotalAmount_Name = "OriginalTotalAmount";
		public const bool OriginalTotalAmount_IsRequired = true;
		public const decimal OriginalTotalAmount_MinValue = 0m;
        public const decimal OriginalTotalAmount_MaxValue = decimal.MaxValue;
		public const double OriginalTotalAmount_MinValue_Double = 0D;
        public const double OriginalTotalAmount_MaxValue_Double = double.MaxValue;

        public const string ShippingAddress_Name = "ShippingAddress";
		public const bool ShippingAddress_IsRequired = true;

		public const string ShippingAddressId_Name = "ShippingAddressId";
		public const bool ShippingAddressId_IsRequired = true;

        public const string Customer_Name = "Customer";
		public const bool Customer_IsRequired = true;

		public const string CustomerId_Name = "CustomerId";
		public const bool CustomerId_IsRequired = true;

        public const string CustomerChargeOption_Name = "CustomerChargeOption";
		public const bool CustomerChargeOption_IsRequired = true;

		public const string CustomerChargeOptionId_Name = "CustomerChargeOptionId";
		public const bool CustomerChargeOptionId_IsRequired = true;

        public const string Charges_Name = "Charges";
		public const bool Charges_IsRequired = false;

        public const string DiscountUsages_Name = "DiscountUsages";
		public const bool DiscountUsages_IsRequired = false;

        public const string OrderItems_Name = "OrderItems";
		public const bool OrderItems_IsRequired = false;

	}
}
