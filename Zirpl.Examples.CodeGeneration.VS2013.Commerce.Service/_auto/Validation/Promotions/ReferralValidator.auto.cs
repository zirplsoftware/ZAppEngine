
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Promotions
{
    public abstract partial class ReferralValidator<T>  : DbEntityValidatorBase<T>
		where T : Referral
    {
        protected ReferralValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.PromoCode, o => o.PromoCodeId,
                ReferralMetadataConstants.PromoCode_Name, ReferralMetadataConstants.PromoCodeId_Name);
			this.When(o =>  o.ReferredCustomerJoinedDate.HasValue, () => {
				this.RuleFor(o => o.ReferredCustomerJoinedDate).NotEmpty();
			});
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferredCustomer, o => o.ReferredCustomerId,
                ReferralMetadataConstants.ReferredCustomer_Name, ReferralMetadataConstants.ReferredCustomerId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferredCustomerAwardDiscount, o => o.ReferredCustomerAwardDiscountId,
                ReferralMetadataConstants.ReferredCustomerAwardDiscount_Name, ReferralMetadataConstants.ReferredCustomerAwardDiscountId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferredCustomerAwardDiscountUsage, o => o.ReferredCustomerAwardDiscountUsageId,
                ReferralMetadataConstants.ReferredCustomerAwardDiscountUsage_Name, ReferralMetadataConstants.ReferredCustomerAwardDiscountUsageId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

