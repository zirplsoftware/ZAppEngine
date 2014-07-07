using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions
{
    public partial class SubscriptionInstanceMapping : CoreEntityMappingBase<SubscriptionInstance, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.StartDate).IsRequired(SubscriptionInstanceMetadata.StartDate_IsRequired).IsDateTime();
			this.Property(o => o.TotalShipments).IsRequired(SubscriptionInstanceMetadata.TotalShipments_IsRequired);
			this.Property(o => o.ShipmentsRemaining).IsRequired(SubscriptionInstanceMetadata.ShipmentsRemaining_IsRequired);

            this.HasNavigationProperty(o => o.Subscription,
                                        o => o.SubscriptionId,
                                        SubscriptionInstanceMetadata.Subscription_IsRequired,
                                        CascadeOnDeleteOption.Yes);

            this.HasNavigationProperty(o => o.PendingSubscriptionChange,
                                        o => o.PendingSubscriptionChangeId,
                                        SubscriptionInstanceMetadata.PendingSubscriptionChange_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
