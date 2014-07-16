
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Partners
{
    public partial class PartnerValidator  : DbEntityValidatorBase<Partner>
		
    {
        public PartnerValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.Visitor, o => o.VisitorId,
                PartnerMetadata.Visitor_Name, PartnerMetadata.VisitorId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Address, o => o.AddressId,
                PartnerMetadata.Address_Name, PartnerMetadata.AddressId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferralPlan, o => o.ReferralPlanId,
                PartnerMetadata.ReferralPlan_Name, PartnerMetadata.ReferralPlanId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.PromoCode, o => o.PromoCodeId,
                PartnerMetadata.PromoCode_Name, PartnerMetadata.PromoCodeId_Name);
			this.RuleFor(o => o.CrmUrl).Length(PartnerMetadata.CrmUrl_MinLength, PartnerMetadata.CrmUrl_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

