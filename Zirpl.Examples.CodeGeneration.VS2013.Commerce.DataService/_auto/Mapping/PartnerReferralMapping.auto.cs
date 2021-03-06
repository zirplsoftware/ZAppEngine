﻿using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Partners
{
    public partial class PartnerReferralMapping : EntityMappingBase<PartnerReferral, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.Request,
                                        o => o.RequestId,
                                        PartnerReferralMetadataConstants.Request_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Partner,
                                        o => o.PartnerId,
                                        PartnerReferralMetadataConstants.Partner_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Plan,
                                        o => o.PlanId,
                                        PartnerReferralMetadataConstants.Plan_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
        protected override bool MapEntityBaseProperties
        {
            get
            {
                return false;
            }
        }
    }
}
