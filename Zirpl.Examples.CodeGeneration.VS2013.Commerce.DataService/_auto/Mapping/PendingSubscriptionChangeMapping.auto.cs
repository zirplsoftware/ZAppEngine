using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions
{
    public partial class PendingSubscriptionChangeMapping : CoreEntityMappingBase<PendingSubscriptionChange, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Quantity).IsRequired(PendingSubscriptionChangeMetadata.Quantity_IsRequired);

            this.HasNavigationProperty(o => o.SubscriptionChoice,
                                        o => o.SubscriptionChoiceId,
                                        PendingSubscriptionChangeMetadata.SubscriptionChoice_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
