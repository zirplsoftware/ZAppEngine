using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Settings
{
    public partial class TaxRuleMapping : CoreEntityMappingBase<TaxRule, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Rate).IsRequired(TaxRuleMetadataConstants.Rate_IsRequired).HasPrecision(18,4);

            this.HasNavigationProperty(o => o.StateProvinceType,
                                        o => o.StateProvinceTypeId,
                                        TaxRuleMetadataConstants.StateProvinceType_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
