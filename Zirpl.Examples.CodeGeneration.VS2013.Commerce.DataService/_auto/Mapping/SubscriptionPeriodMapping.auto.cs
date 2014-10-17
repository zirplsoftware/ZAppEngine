using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions
{
    public partial class SubscriptionPeriodMapping : CoreEntityMappingBase<SubscriptionPeriod, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.ChargePeriod).IsRequired(SubscriptionPeriodMetadataConstants.ChargePeriod_IsRequired);

            this.HasNavigationProperty(o => o.ChargePeriodType,
                                        o => o.ChargePeriodTypeId,
                                        SubscriptionPeriodMetadataConstants.ChargePeriodType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.ShipmentPeriod).IsRequired(SubscriptionPeriodMetadataConstants.ShipmentPeriod_IsRequired);

            this.HasNavigationProperty(o => o.ShipmentPeriodType,
                                        o => o.ShipmentPeriodTypeId,
                                        SubscriptionPeriodMetadataConstants.ShipmentPeriodType_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
