using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Subscriptions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Subscriptions
{
    public partial class SubscriptionPeriodTypeMapping : DictionaryEntityMapping<SubscriptionPeriodType, byte, SubscriptionPeriodTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(SubscriptionPeriodTypeMetadataConstants.Name_IsRequired).HasMaxLength(SubscriptionPeriodTypeMetadataConstants.Name_MaxLength, SubscriptionPeriodTypeMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.PluralName).IsRequired(SubscriptionPeriodTypeMetadataConstants.PluralName_IsRequired).HasMaxLength(SubscriptionPeriodTypeMetadataConstants.PluralName_MaxLength, SubscriptionPeriodTypeMetadataConstants.PluralName_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
