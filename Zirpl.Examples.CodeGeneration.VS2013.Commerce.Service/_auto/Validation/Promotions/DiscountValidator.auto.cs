
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Promotions
{
    public partial class DiscountValidator  : DbEntityValidatorBase<Discount>
		
    {
        public DiscountValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(DiscountMetadataConstants.Name_MinLength, DiscountMetadataConstants.Name_MaxLength);
            this.ForeignEntityNotNullAndIdMatches(o => o.PromoCode, o => o.PromoCodeId,
                DiscountMetadataConstants.PromoCode_Name, DiscountMetadataConstants.PromoCodeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.DiscountApplicabilityType, o => o.DiscountApplicabilityTypeId,
                DiscountMetadataConstants.DiscountApplicabilityType_Name, DiscountMetadataConstants.DiscountApplicabilityTypeId_Name);
			this.RuleFor(o => o.Amount).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(DiscountMetadataConstants.Amount_MinValue, DiscountMetadataConstants.Amount_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.DiscountAmountType, o => o.DiscountAmountTypeId,
                DiscountMetadataConstants.DiscountAmountType_Name, DiscountMetadataConstants.DiscountAmountTypeId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.DiscountUsageRestrictionType, o => o.DiscountUsageRestrictionTypeId,
                DiscountMetadataConstants.DiscountUsageRestrictionType_Name, DiscountMetadataConstants.DiscountUsageRestrictionTypeId_Name);
			this.RuleFor(o => o.DiscountUsageRestrictionQuantity).InclusiveBetween(DiscountMetadataConstants.DiscountUsageRestrictionQuantity_MinValue, DiscountMetadataConstants.DiscountUsageRestrictionQuantity_MaxValue);
			this.RuleFor(o => o.StopAfterChargeCyles).InclusiveBetween(DiscountMetadataConstants.StopAfterChargeCyles_MinValue, DiscountMetadataConstants.StopAfterChargeCyles_MaxValue);
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

