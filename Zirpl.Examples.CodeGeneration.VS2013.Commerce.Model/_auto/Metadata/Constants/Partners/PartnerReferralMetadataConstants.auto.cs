using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Partners
{
    public partial class PartnerReferralMetadataConstants : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Promotions.ReferralMetadataConstants
    {
        public const string Request_Name = "Request";
		public const bool Request_IsRequired = false;

		public const string RequestId_Name = "RequestId";
		public const bool RequestId_IsRequired = false;

        public const string Partner_Name = "Partner";
		public const bool Partner_IsRequired = true;

		public const string PartnerId_Name = "PartnerId";
		public const bool PartnerId_IsRequired = true;

        public const string Plan_Name = "Plan";
		public const bool Plan_IsRequired = false;

		public const string PlanId_Name = "PlanId";
		public const bool PlanId_IsRequired = false;

	}
}
