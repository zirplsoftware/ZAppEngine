using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners
{
    public partial class PartnerMapping : CoreEntityMappingBase<Partner, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Visitor,
                                        o => o.VisitorId,
                                        PartnerMetadata.Visitor_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Address,
                                        o => o.AddressId,
                                        PartnerMetadata.Address_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ReferralPlan,
                                        o => o.ReferralPlanId,
                                        PartnerMetadata.ReferralPlan_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.PromoCode,
                                        o => o.PromoCodeId,
                                        PartnerMetadata.PromoCode_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.CrmUrl).IsRequired(PartnerMetadata.CrmUrl_IsRequired).HasMaxLength(PartnerMetadata.CrmUrl_MaxLength, PartnerMetadata.CrmUrl_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
