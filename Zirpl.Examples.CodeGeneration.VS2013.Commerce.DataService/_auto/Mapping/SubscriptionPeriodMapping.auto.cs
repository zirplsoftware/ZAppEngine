using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions
{
    public partial class SubscriptionPeriodMapping : CoreEntityMappingBase<SubscriptionPeriod, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.ChargePeriod).IsRequired(SubscriptionPeriodMetadata.ChargePeriod_IsRequired);

            this.HasNavigationProperty(o => o.ChargePeriodType,
                                        o => o.ChargePeriodTypeId,
                                        SubscriptionPeriodMetadata.ChargePeriodType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.ShipmentPeriod).IsRequired(SubscriptionPeriodMetadata.ShipmentPeriod_IsRequired);

            this.HasNavigationProperty(o => o.ShipmentPeriodType,
                                        o => o.ShipmentPeriodTypeId,
                                        SubscriptionPeriodMetadata.ShipmentPeriodType_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
