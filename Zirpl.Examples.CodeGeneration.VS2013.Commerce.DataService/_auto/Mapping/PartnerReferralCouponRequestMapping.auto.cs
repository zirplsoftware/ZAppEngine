﻿using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners
{
    public partial class PartnerReferralCouponRequestMapping : CoreEntityMappingBase<PartnerReferralCouponRequest, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.RequestDate).IsRequired(PartnerReferralCouponRequestMetadata.RequestDate_IsRequired).IsDateTime();
			this.Property(o => o.Quantity).IsRequired(PartnerReferralCouponRequestMetadata.Quantity_IsRequired);
			this.Property(o => o.ShippedDate).IsRequired(PartnerReferralCouponRequestMetadata.ShippedDate_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.Partner,
                                        o => o.PartnerId,
                                        PartnerReferralCouponRequestMetadata.Partner_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.PartnerReferralCouponRequestStatusType,
                                        o => o.PartnerReferralCouponRequestStatusTypeId,
                                        PartnerReferralCouponRequestMetadata.PartnerReferralCouponRequestStatusType_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}