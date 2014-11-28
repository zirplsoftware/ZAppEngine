using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Partners
{
    public partial class PartnerReferralCouponRequestMetadataConstants : MetadataConstantsBase
    {
        public const string RequestDate_Name = "RequestDate";
		public const bool RequestDate_IsRequired = true;

        public const string Quantity_Name = "Quantity";
		public const bool Quantity_IsRequired = true;
		public const int Quantity_MinValue = 1;
        public const int Quantity_MaxValue = int.MaxValue;

        public const string ShippedDate_Name = "ShippedDate";
		public const bool ShippedDate_IsRequired = false;

        public const string Partner_Name = "Partner";
		public const bool Partner_IsRequired = true;

		public const string PartnerId_Name = "PartnerId";
		public const bool PartnerId_IsRequired = true;

        public const string PartnerReferralCouponRequestStatusType_Name = "PartnerReferralCouponRequestStatusType";
		public const bool PartnerReferralCouponRequestStatusType_IsRequired = true;

		public const string PartnerReferralCouponRequestStatusTypeId_Name = "PartnerReferralCouponRequestStatusTypeId";
		public const bool PartnerReferralCouponRequestStatusTypeId_IsRequired = true;

	}
}
