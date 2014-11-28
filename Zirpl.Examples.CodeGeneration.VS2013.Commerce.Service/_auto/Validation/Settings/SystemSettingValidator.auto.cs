
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Settings
{
    public partial class SystemSettingValidator  : DbEntityValidatorBase<SystemSetting>
		
    {
        public SystemSettingValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(SystemSettingMetadataConstants.Name_MinLength, SystemSettingMetadataConstants.Name_MaxLength);
			this.RuleFor(o => o.Value).Length(SystemSettingMetadataConstants.Value_MinLength, SystemSettingMetadataConstants.Value_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

