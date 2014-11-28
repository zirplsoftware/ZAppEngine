using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Partners
{
    public partial class PartnerReferralPlanMetadataConstants : MetadataConstantsBase
    {
        public const string Name_Name = "Name";
		public const bool Name_IsRequired = true;
		public const bool Name_IsMaxLength = false;
        public const int Name_MinLength = 1;
		public const int Name_MaxLength = 256;

        public const string Amount_Name = "Amount";
		public const bool Amount_IsRequired = true;
		public const decimal Amount_MinValue = 0.00m;
        public const decimal Amount_MaxValue = decimal.MaxValue;
		public const double Amount_MinValue_Double = 0.00D;
        public const double Amount_MaxValue_Double = double.MaxValue;

        public const string ReferredCustomerAwardDiscount_Name = "ReferredCustomerAwardDiscount";
		public const bool ReferredCustomerAwardDiscount_IsRequired = true;

		public const string ReferredCustomerAwardDiscountId_Name = "ReferredCustomerAwardDiscountId";
		public const bool ReferredCustomerAwardDiscountId_IsRequired = true;

	}
}
