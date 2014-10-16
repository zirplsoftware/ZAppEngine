using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class PartnerMetadataConstants : MetadataConstantsBase
    {
        public const string Visitor_Name = "Visitor";
		public const bool Visitor_IsRequired = true;

		public const string VisitorId_Name = "VisitorId";
		public const bool VisitorId_IsRequired = true;

        public const string Address_Name = "Address";
		public const bool Address_IsRequired = true;

		public const string AddressId_Name = "AddressId";
		public const bool AddressId_IsRequired = true;

        public const string ReferralPlan_Name = "ReferralPlan";
		public const bool ReferralPlan_IsRequired = false;

		public const string ReferralPlanId_Name = "ReferralPlanId";
		public const bool ReferralPlanId_IsRequired = false;

        public const string PromoCode_Name = "PromoCode";
		public const bool PromoCode_IsRequired = true;

		public const string PromoCodeId_Name = "PromoCodeId";
		public const bool PromoCodeId_IsRequired = true;

        public const string CrmUrl_Name = "CrmUrl";
		public const bool CrmUrl_IsRequired = false;
		public const bool CrmUrl_IsMaxLength = false;
        public const int CrmUrl_MinLength = 0;
		public const int CrmUrl_MaxLength = 256;

	}
}
