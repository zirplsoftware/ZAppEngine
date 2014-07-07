using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners
{
    public partial class PartnerReferralMapping : CoreEntityMappingBase<PartnerReferral, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Request,
                                        o => o.RequestId,
                                        PartnerReferralMetadata.Request_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Partner,
                                        o => o.PartnerId,
                                        PartnerReferralMetadata.Partner_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Plan,
                                        o => o.PlanId,
                                        PartnerReferralMetadata.Plan_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
        protected override bool MapCoreEntityBaseProperties
        {
            get
            {
                return false;
            }
        }
    }
}
