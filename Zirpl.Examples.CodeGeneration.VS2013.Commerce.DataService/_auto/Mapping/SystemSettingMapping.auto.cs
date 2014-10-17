using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Settings
{
    public partial class SystemSettingMapping : CoreEntityMappingBase<SystemSetting, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(SystemSettingMetadataConstants.Name_IsRequired).HasMaxLength(SystemSettingMetadataConstants.Name_MaxLength, SystemSettingMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.Value).IsRequired(SystemSettingMetadataConstants.Value_IsRequired).HasMaxLength(SystemSettingMetadataConstants.Value_MaxLength, SystemSettingMetadataConstants.Value_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
