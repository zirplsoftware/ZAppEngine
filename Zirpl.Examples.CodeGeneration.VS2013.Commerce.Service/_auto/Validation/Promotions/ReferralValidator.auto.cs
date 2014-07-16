
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Promotions
{
    public abstract partial class ReferralValidator<T>  : DbEntityValidatorBase<T>
		where T : Referral
    {
        protected ReferralValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.PromoCode, o => o.PromoCodeId,
                ReferralMetadata.PromoCode_Name, ReferralMetadata.PromoCodeId_Name);
			this.When(o =>  o.ReferredCustomerJoinedDate.HasValue, () => {
				this.RuleFor(o => o.ReferredCustomerJoinedDate).NotEmpty();
			});
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferredCustomer, o => o.ReferredCustomerId,
                ReferralMetadata.ReferredCustomer_Name, ReferralMetadata.ReferredCustomerId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferredCustomerAwardDiscount, o => o.ReferredCustomerAwardDiscountId,
                ReferralMetadata.ReferredCustomerAwardDiscount_Name, ReferralMetadata.ReferredCustomerAwardDiscountId_Name);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.ReferredCustomerAwardDiscountUsage, o => o.ReferredCustomerAwardDiscountUsageId,
                ReferralMetadata.ReferredCustomerAwardDiscountUsage_Name, ReferralMetadata.ReferredCustomerAwardDiscountUsageId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

