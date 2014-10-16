using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners
{
    public partial class PartnerReferralPlanMapping : CoreEntityMappingBase<PartnerReferralPlan, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(PartnerReferralPlanMetadataConstants.Name_IsRequired).HasMaxLength(PartnerReferralPlanMetadataConstants.Name_MaxLength, PartnerReferralPlanMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.Amount).IsRequired(PartnerReferralPlanMetadataConstants.Amount_IsRequired).IsCurrency();

            this.HasNavigationProperty(o => o.ReferredCustomerAwardDiscount,
                                        o => o.ReferredCustomerAwardDiscountId,
                                        PartnerReferralPlanMetadataConstants.ReferredCustomerAwardDiscount_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
