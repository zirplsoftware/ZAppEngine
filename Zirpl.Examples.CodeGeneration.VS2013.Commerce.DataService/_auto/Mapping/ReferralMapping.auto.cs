using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions
{
    public partial class ReferralMapping : CoreEntityMappingBase<Referral, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.PromoCode,
                                        o => o.PromoCodeId,
                                        ReferralMetadataConstants.PromoCode_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.ReferredCustomerJoinedDate).IsRequired(ReferralMetadataConstants.ReferredCustomerJoinedDate_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.ReferredCustomer,
                                        o => o.ReferredCustomerId,
                                        ReferralMetadataConstants.ReferredCustomer_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ReferredCustomerAwardDiscount,
                                        o => o.ReferredCustomerAwardDiscountId,
                                        ReferralMetadataConstants.ReferredCustomerAwardDiscount_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ReferredCustomerAwardDiscountUsage,
                                        o => o.ReferredCustomerAwardDiscountUsageId,
                                        ReferralMetadataConstants.ReferredCustomerAwardDiscountUsage_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
