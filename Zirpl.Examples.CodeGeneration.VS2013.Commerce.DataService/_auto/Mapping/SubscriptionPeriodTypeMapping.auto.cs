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

			this.Property(o => o.Name).IsRequired(SubscriptionPeriodTypeMetadata.Name_IsRequired).HasMaxLength(SubscriptionPeriodTypeMetadata.Name_MaxLength, SubscriptionPeriodTypeMetadata.Name_IsMaxLength);
			this.Property(o => o.PluralName).IsRequired(SubscriptionPeriodTypeMetadata.PluralName_IsRequired).HasMaxLength(SubscriptionPeriodTypeMetadata.PluralName_MaxLength, SubscriptionPeriodTypeMetadata.PluralName_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
