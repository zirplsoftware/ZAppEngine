using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions
{
    public partial class SubscriptionMapping : CoreEntityMappingBase<Subscription, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.StartDate).IsRequired(SubscriptionMetadata.StartDate_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.StatusType,
                                        o => o.StatusTypeId,
                                        SubscriptionMetadata.StatusType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.NextShipmentDate).IsRequired(SubscriptionMetadata.NextShipmentDate_IsRequired).IsDateTime();
			this.Property(o => o.NextChargeDate).IsRequired(SubscriptionMetadata.NextChargeDate_IsRequired).IsDateTime();
			this.Property(o => o.AutoRenew).IsRequired(SubscriptionMetadata.AutoRenew_IsRequired);

            this.HasNavigationProperty(o => o.Customer,
                                        o => o.CustomerId,
                                        SubscriptionMetadata.Customer_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ShippingAddress,
                                        o => o.ShippingAddressId,
                                        SubscriptionMetadata.ShippingAddress_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CustomerChargeOption,
                                        o => o.CustomerChargeOptionId,
                                        SubscriptionMetadata.CustomerChargeOption_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CurrentSubscriptionInstance,
                                        o => o.CurrentSubscriptionInstanceId,
                                        SubscriptionMetadata.CurrentSubscriptionInstance_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
