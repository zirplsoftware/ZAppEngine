using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class TierPriceMapping : CoreEntityMappingBase<TierPrice, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.DisplayProduct,
                                        o => o.DisplayProductId,
                                        TierPriceMetadata.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Quantity).IsRequired(TierPriceMetadata.Quantity_IsRequired);
			this.Property(o => o.PriceEach).IsRequired(TierPriceMetadata.PriceEach_IsRequired).IsCurrency();

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
