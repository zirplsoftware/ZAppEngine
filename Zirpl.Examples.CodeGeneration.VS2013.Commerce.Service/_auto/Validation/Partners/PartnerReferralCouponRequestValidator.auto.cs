
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Partners
{
    public partial class PartnerReferralCouponRequestValidator  : DbEntityValidatorBase<PartnerReferralCouponRequest>
		
    {
        public PartnerReferralCouponRequestValidator()
        {
			this.RuleFor(o => o.RequestDate).NotEmpty();
			this.RuleFor(o => o.Quantity).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(PartnerReferralCouponRequestMetadataConstants.Quantity_MinValue, PartnerReferralCouponRequestMetadataConstants.Quantity_MaxValue);
			this.When(o =>  o.ShippedDate.HasValue, () => {
				this.RuleFor(o => o.ShippedDate).NotEmpty();
			});
            this.ForeignEntityNotNullAndIdMatches(o => o.Partner, o => o.PartnerId,
                PartnerReferralCouponRequestMetadataConstants.Partner_Name, PartnerReferralCouponRequestMetadataConstants.PartnerId_Name);
            this.ForeignEntityNotNullAndIdMatches(o => o.PartnerReferralCouponRequestStatusType, o => o.PartnerReferralCouponRequestStatusTypeId,
                PartnerReferralCouponRequestMetadataConstants.PartnerReferralCouponRequestStatusType_Name, PartnerReferralCouponRequestMetadataConstants.PartnerReferralCouponRequestStatusTypeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

