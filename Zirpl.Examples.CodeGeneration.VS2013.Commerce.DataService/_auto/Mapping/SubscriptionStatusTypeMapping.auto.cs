using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions
{
    public partial class SubscriptionStatusTypeMapping : DictionaryEntityMapping<SubscriptionStatusType, byte, SubscriptionStatusTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(SubscriptionStatusTypeMetadataConstants.Name_IsRequired).HasMaxLength(SubscriptionStatusTypeMetadataConstants.Name_MaxLength, SubscriptionStatusTypeMetadataConstants.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
