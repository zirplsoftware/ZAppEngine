
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Partners
{
    public partial class PartnerReferralCouponRequestValidator  : DbEntityValidatorBase<PartnerReferralCouponRequest>
		
    {
        public PartnerReferralCouponRequestValidator()
        {
			this.RuleFor(o => o.RequestDate).NotEmpty();
			this.RuleFor(o => o.Quantity).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(PartnerReferralCouponRequestMetadata.Quantity_MinValue, PartnerReferralCouponRequestMetadata.Quantity_MaxValue);
			this.When(o =>  o.ShippedDate.HasValue, () => {
				this.RuleFor(o => o.ShippedDate).NotEmpty();
			});
            this.ForeignEntityNotNullAndIdMatches(o => o.Partner, o => o.PartnerId,
                PartnerReferralCouponRequestMetadata.Partner_Name, PartnerReferralCouponRequestMetadata.PartnerId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.PartnerReferralCouponRequestStatusType, o => o.PartnerReferralCouponRequestStatusTypeId,
                PartnerReferralCouponRequestMetadata.PartnerReferralCouponRequestStatusType_Name, PartnerReferralCouponRequestMetadata.PartnerReferralCouponRequestStatusTypeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

