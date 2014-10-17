
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Settings
{
    public partial class TaxRuleValidator  : DbEntityValidatorBase<TaxRule>
		
    {
        public TaxRuleValidator()
        {
			this.RuleFor(o => o.Rate).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(TaxRuleMetadataConstants.Rate_MinValue, TaxRuleMetadataConstants.Rate_MaxValue);
            this.ForeignEntityNotNullAndIdMatches(o => o.StateProvinceType, o => o.StateProvinceTypeId,
                TaxRuleMetadataConstants.StateProvinceType_Name, TaxRuleMetadataConstants.StateProvinceTypeId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

