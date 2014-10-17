using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions
{
    public partial class SubscriptionMapping : CoreEntityMappingBase<Subscription, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.StartDate).IsRequired(SubscriptionMetadataConstants.StartDate_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.StatusType,
                                        o => o.StatusTypeId,
                                        SubscriptionMetadataConstants.StatusType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.NextShipmentDate).IsRequired(SubscriptionMetadataConstants.NextShipmentDate_IsRequired).IsDateTime();
			this.Property(o => o.NextChargeDate).IsRequired(SubscriptionMetadataConstants.NextChargeDate_IsRequired).IsDateTime();
			this.Property(o => o.AutoRenew).IsRequired(SubscriptionMetadataConstants.AutoRenew_IsRequired);

            this.HasNavigationProperty(o => o.Customer,
                                        o => o.CustomerId,
                                        SubscriptionMetadataConstants.Customer_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ShippingAddress,
                                        o => o.ShippingAddressId,
                                        SubscriptionMetadataConstants.ShippingAddress_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CustomerChargeOption,
                                        o => o.CustomerChargeOptionId,
                                        SubscriptionMetadataConstants.CustomerChargeOption_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CurrentSubscriptionInstance,
                                        o => o.CurrentSubscriptionInstanceId,
                                        SubscriptionMetadataConstants.CurrentSubscriptionInstance_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
