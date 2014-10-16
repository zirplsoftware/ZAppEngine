using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers
{
    public partial class CustomerMetadataConstants : MetadataConstantsBase
    {
        public const string Visitor_Name = "Visitor";
		public const bool Visitor_IsRequired = true;

		public const string VisitorId_Name = "VisitorId";
		public const bool VisitorId_IsRequired = true;

        public const string PromoCode_Name = "PromoCode";
		public const bool PromoCode_IsRequired = true;

		public const string PromoCodeId_Name = "PromoCodeId";
		public const bool PromoCodeId_IsRequired = true;

        public const string CurrentShippingAddress_Name = "CurrentShippingAddress";
		public const bool CurrentShippingAddress_IsRequired = false;

		public const string CurrentShippingAddressId_Name = "CurrentShippingAddressId";
		public const bool CurrentShippingAddressId_IsRequired = false;

        public const string CurrentCustomerChargeOption_Name = "CurrentCustomerChargeOption";
		public const bool CurrentCustomerChargeOption_IsRequired = false;

		public const string CurrentCustomerChargeOptionId_Name = "CurrentCustomerChargeOptionId";
		public const bool CurrentCustomerChargeOptionId_IsRequired = false;

	}
}
