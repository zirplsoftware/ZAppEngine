using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Settings
{
    public partial class SystemSettingMapping : CoreEntityMappingBase<SystemSetting, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(SystemSettingMetadata.Name_IsRequired).HasMaxLength(SystemSettingMetadata.Name_MaxLength, SystemSettingMetadata.Name_IsMaxLength);
			this.Property(o => o.Value).IsRequired(SystemSettingMetadata.Value_IsRequired).HasMaxLength(SystemSettingMetadata.Value_MaxLength, SystemSettingMetadata.Value_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
