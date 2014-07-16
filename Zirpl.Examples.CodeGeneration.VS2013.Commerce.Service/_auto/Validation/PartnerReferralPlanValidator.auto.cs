
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
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(PartnerReferralPlanMetadata.Name_MinLength, PartnerReferralPlanMetadata.Name_MaxLength);
			this.RuleFor(o => o.Amount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(PartnerReferralPlanMetadata.Amount_MinValue, PartnerReferralPlanMetadata.Amount_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.ReferredCustomerAwardDiscount, o => o.ReferredCustomerAwardDiscountId,
                PartnerReferralPlanMetadata.ReferredCustomerAwardDiscount_Name, PartnerReferralPlanMetadata.ReferredCustomerAwardDiscountId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

