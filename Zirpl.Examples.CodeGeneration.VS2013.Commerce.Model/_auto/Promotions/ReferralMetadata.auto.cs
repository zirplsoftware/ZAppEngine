using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions
{
    public abstract partial class ReferralMetadata : MetadataBase
    {
        public const string PromoCode_Name = "PromoCode";
		public const bool PromoCode_IsRequired = true;

		public const string PromoCodeId_Name = "PromoCodeId";
		public const bool PromoCodeId_IsRequired = true;

        public const string ReferredCustomerJoinedDate_Name = "ReferredCustomerJoinedDate";
		public const bool ReferredCustomerJoinedDate_IsRequired = false;

        public const string ReferredCustomer_Name = "ReferredCustomer";
		public const bool ReferredCustomer_IsRequired = false;

		public const string ReferredCustomerId_Name = "ReferredCustomerId";
		public const bool ReferredCustomerId_IsRequired = false;

        public const string ReferredCustomerAwardDiscount_Name = "ReferredCustomerAwardDiscount";
		public const bool ReferredCustomerAwardDiscount_IsRequired = false;

		public const string ReferredCustomerAwardDiscountId_Name = "ReferredCustomerAwardDiscountId";
		public const bool ReferredCustomerAwardDiscountId_IsRequired = false;

        public const string ReferredCustomerAwardDiscountUsage_Name = "ReferredCustomerAwardDiscountUsage";
		public const bool ReferredCustomerAwardDiscountUsage_IsRequired = false;

		public const string ReferredCustomerAwardDiscountUsageId_Name = "ReferredCustomerAwardDiscountUsageId";
		public const bool ReferredCustomerAwardDiscountUsageId_IsRequired = false;

	}
}
