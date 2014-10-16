using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class SubscriptionChoiceTierPriceCombinationMapping : CoreEntityMappingBase<SubscriptionChoiceTierPriceCombination, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.SubscriptionChoice,
                                        o => o.SubscriptionChoiceId,
                                        SubscriptionChoiceTierPriceCombinationMetadataConstants.SubscriptionChoice_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.TierPrice,
                                        o => o.TierPriceId,
                                        SubscriptionChoiceTierPriceCombinationMetadataConstants.TierPrice_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.PriceEach).IsRequired(SubscriptionChoiceTierPriceCombinationMetadataConstants.PriceEach_IsRequired).IsCurrency();

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
