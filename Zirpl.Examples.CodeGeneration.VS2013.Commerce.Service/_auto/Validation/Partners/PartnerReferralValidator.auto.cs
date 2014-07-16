
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Partners
{
    public partial class PartnerReferralValidator  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Promotions.ReferralValidator<PartnerReferral>
		
    {
        public PartnerReferralValidator()
        {
            this.ForeignEntityAndIdMatchIfNotNull(o => o.Request, o => o.RequestId,
                PartnerReferralMetadata.Request_Name, PartnerReferralMetadata.RequestId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Partner, o => o.PartnerId,
                PartnerReferralMetadata.Partner_Name, PartnerReferralMetadata.PartnerId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.Plan, o => o.PlanId,
                PartnerReferralMetadata.Plan_Name, PartnerReferralMetadata.PlanId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

