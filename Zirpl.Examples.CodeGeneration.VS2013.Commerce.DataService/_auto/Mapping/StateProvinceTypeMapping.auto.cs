using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Settings
{
    public partial class StateProvinceTypeMapping : DictionaryEntityMapping<StateProvinceType, int, StateProvinceTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(StateProvinceTypeMetadataConstants.Name_IsRequired).HasMaxLength(StateProvinceTypeMetadataConstants.Name_MaxLength, StateProvinceTypeMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.Abbreviation).IsRequired(StateProvinceTypeMetadataConstants.Abbreviation_IsRequired).HasMaxLength(StateProvinceTypeMetadataConstants.Abbreviation_MaxLength, StateProvinceTypeMetadataConstants.Abbreviation_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
