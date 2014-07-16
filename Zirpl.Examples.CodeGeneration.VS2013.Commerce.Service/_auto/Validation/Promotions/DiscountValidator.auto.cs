
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Promotions
{
    public partial class DiscountValidator  : DbEntityValidatorBase<Discount>
		
    {
        public DiscountValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(DiscountMetadata.Name_MinLength, DiscountMetadata.Name_MaxLength);
            this.ForeignEntityNotNullAndIdMatches(o => o.PromoCode, o => o.PromoCodeId,
                DiscountMetadata.PromoCode_Name, DiscountMetadata.PromoCodeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.DiscountApplicabilityType, o => o.DiscountApplicabilityTypeId,
                DiscountMetadata.DiscountApplicabilityType_Name, DiscountMetadata.DiscountApplicabilityTypeId_Name);
			this.RuleFor(o => o.Amount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(DiscountMetadata.Amount_MinValue, DiscountMetadata.Amount_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.DiscountAmountType, o => o.DiscountAmountTypeId,
                DiscountMetadata.DiscountAmountType_Name, DiscountMetadata.DiscountAmountTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.DiscountUsageRestrictionType, o => o.DiscountUsageRestrictionTypeId,
                DiscountMetadata.DiscountUsageRestrictionType_Name, DiscountMetadata.DiscountUsageRestrictionTypeId_Name);
			this.RuleFor(o => o.DiscountUsageRestrictionQuantity).InclusiveBetween(DiscountMetadata.DiscountUsageRestrictionQuantity_MinValue, DiscountMetadata.DiscountUsageRestrictionQuantity_MaxValue);
			this.RuleFor(o => o.StopAfterChargeCyles).InclusiveBetween(DiscountMetadata.StopAfterChargeCyles_MinValue, DiscountMetadata.StopAfterChargeCyles_MaxValue);
			this.When(o =>  o.StartDate.HasValue, () => {
				this.RuleFor(o => o.StartDate).NotEmpty();
			});
			this.When(o =>  o.EndDate.HasValue, () => {
				this.RuleFor(o => o.EndDate).NotEmpty();
			});
			this.RuleFor(o => o.Published).NotNull();
			// unsure how to follow this for validation or even if it should with EF- Collection property: AppliesToDisplayProducts

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

