using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class SubscriptionChoiceMapping : CoreEntityMappingBase<SubscriptionChoice, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.DisplayProduct,
                                        o => o.DisplayProductId,
                                        SubscriptionChoiceMetadata.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.SubscriptionPeriod,
                                        o => o.SubscriptionPeriodId,
                                        SubscriptionChoiceMetadata.SubscriptionPeriod_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.PriceEach).IsRequired(SubscriptionChoiceMetadata.PriceEach_IsRequired).IsCurrency();

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
