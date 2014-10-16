using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class TierShipmentMapping : CoreEntityMappingBase<TierShipment, int>
    {
		protected override void MapProperties()
        {


            this.HasNavigationProperty(o => o.DisplayProduct,
                                        o => o.DisplayProductId,
                                        TierShipmentMetadataConstants.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Quantity).IsRequired(TierShipmentMetadataConstants.Quantity_IsRequired);
			this.Property(o => o.BaseWeightInOunces).IsRequired(TierShipmentMetadataConstants.BaseWeightInOunces_IsRequired);
			this.Property(o => o.WeightInOuncesEach).IsRequired(TierShipmentMetadataConstants.WeightInOuncesEach_IsRequired);
			this.Property(o => o.RequiresManualShipmentHandling).IsRequired(TierShipmentMetadataConstants.RequiresManualShipmentHandling_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
