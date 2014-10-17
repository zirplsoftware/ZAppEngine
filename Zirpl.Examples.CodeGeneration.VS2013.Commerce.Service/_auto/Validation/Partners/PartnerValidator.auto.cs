
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Partners
{
    public partial class PartnerValidator  : DbEntityValidatorBase<Partner>
		
    {
        public PartnerValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.Visitor, o => o.VisitorId,
                PartnerMetadataConstants.Visitor_Name, PartnerMetadataConstants.VisitorId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.Address, o => o.AddressId,
                PartnerMetadataConstants.Address_Name, PartnerMetadataConstants.AddressId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferralPlan, o => o.ReferralPlanId,
                PartnerMetadataConstants.ReferralPlan_Name, PartnerMetadataConstants.ReferralPlanId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.PromoCode, o => o.PromoCodeId,
                PartnerMetadataConstants.PromoCode_Name, PartnerMetadataConstants.PromoCodeId_Name);
			this.RuleFor(o => o.CrmUrl).Length(PartnerMetadataConstants.CrmUrl_MinLength, PartnerMetadataConstants.CrmUrl_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

