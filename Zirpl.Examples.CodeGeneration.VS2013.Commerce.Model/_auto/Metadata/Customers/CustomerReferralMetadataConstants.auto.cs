using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Customers
{
    public partial class CustomerReferralMetadataConstants : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Promotions.ReferralMetadataConstants
    {
        public const string ReferringCustomer_Name = "ReferringCustomer";
		public const bool ReferringCustomer_IsRequired = true;

		public const string ReferringCustomerId_Name = "ReferringCustomerId";
		public const bool ReferringCustomerId_IsRequired = true;

        public const string ReferringCustomerDiscountAward_Name = "ReferringCustomerDiscountAward";
		public const bool ReferringCustomerDiscountAward_IsRequired = false;

		public const string ReferringCustomerDiscountAwardId_Name = "ReferringCustomerDiscountAwardId";
		public const bool ReferringCustomerDiscountAwardId_IsRequired = false;

        public const string ReferringCustomerDiscountAwardUsage_Name = "ReferringCustomerDiscountAwardUsage";
		public const bool ReferringCustomerDiscountAwardUsage_IsRequired = false;

		public const string ReferringCustomerDiscountAwardUsageId_Name = "ReferringCustomerDiscountAwardUsageId";
		public const bool ReferringCustomerDiscountAwardUsageId_IsRequired = false;

	}
}
