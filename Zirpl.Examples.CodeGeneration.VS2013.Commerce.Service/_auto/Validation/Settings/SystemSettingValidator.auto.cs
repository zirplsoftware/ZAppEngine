
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Settings
{
    public partial class SystemSettingValidator  : DbEntityValidatorBase<SystemSetting>
		
    {
        public SystemSettingValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(SystemSettingMetadata.Name_MinLength, SystemSettingMetadata.Name_MaxLength);
			this.RuleFor(o => o.Value).Length(SystemSettingMetadata.Value_MinLength, SystemSettingMetadata.Value_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

