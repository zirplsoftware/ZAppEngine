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

			this.Property(o => o.StartDate).IsRequired(SubscriptionInstanceMetadataConstants.StartDate_IsRequired).IsDateTime();
			this.Property(o => o.TotalShipments).IsRequired(SubscriptionInstanceMetadataConstants.TotalShipments_IsRequired);
			this.Property(o => o.ShipmentsRemaining).IsRequired(SubscriptionInstanceMetadataConstants.ShipmentsRemaining_IsRequired);

            this.HasNavigationProperty(o => o.Subscription,
                                        o => o.SubscriptionId,
                                        SubscriptionInstanceMetadataConstants.Subscription_IsRequired,
                                        CascadeOnDeleteOption.Yes);

            this.HasNavigationProperty(o => o.PendingSubscriptionChange,
                                        o => o.PendingSubscriptionChangeId,
                                        SubscriptionInstanceMetadataConstants.PendingSubscriptionChange_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
