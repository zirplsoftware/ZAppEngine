
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Partners
{
    public partial class PartnerReferralPlanValidator  : DbEntityValidatorBase<PartnerReferralPlan>
		
    {
        public PartnerReferralPlanValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(PartnerReferralPlanMetadataConstants.Name_MinLength, PartnerReferralPlanMetadataConstants.Name_MaxLength);
			this.RuleFor(o => o.Amount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(PartnerReferralPlanMetadataConstants.Amount_MinValue, PartnerReferralPlanMetadataConstants.Amount_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ReferredCustomerAwardDiscount, o => o.ReferredCustomerAwardDiscountId,
                PartnerReferralPlanMetadataConstants.ReferredCustomerAwardDiscount_Name, PartnerReferralPlanMetadataConstants.ReferredCustomerAwardDiscountId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

