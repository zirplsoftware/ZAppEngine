using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners
{
    public partial class PartnerMapping : CoreEntityMappingBase<Partner, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Visitor,
                                        o => o.VisitorId,
                                        PartnerMetadataConstants.Visitor_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Address,
                                        o => o.AddressId,
                                        PartnerMetadataConstants.Address_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ReferralPlan,
                                        o => o.ReferralPlanId,
                                        PartnerMetadataConstants.ReferralPlan_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.PromoCode,
                                        o => o.PromoCodeId,
                                        PartnerMetadataConstants.PromoCode_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.CrmUrl).IsRequired(PartnerMetadataConstants.CrmUrl_IsRequired).HasMaxLength(PartnerMetadataConstants.CrmUrl_MaxLength, PartnerMetadataConstants.CrmUrl_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
