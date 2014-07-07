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
                                        ReferralMetadata.PromoCode_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.ReferredCustomerJoinedDate).IsRequired(ReferralMetadata.ReferredCustomerJoinedDate_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.ReferredCustomer,
                                        o => o.ReferredCustomerId,
                                        ReferralMetadata.ReferredCustomer_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ReferredCustomerAwardDiscount,
                                        o => o.ReferredCustomerAwardDiscountId,
                                        ReferralMetadata.ReferredCustomerAwardDiscount_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ReferredCustomerAwardDiscountUsage,
                                        o => o.ReferredCustomerAwardDiscountUsageId,
                                        ReferralMetadata.ReferredCustomerAwardDiscountUsage_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
