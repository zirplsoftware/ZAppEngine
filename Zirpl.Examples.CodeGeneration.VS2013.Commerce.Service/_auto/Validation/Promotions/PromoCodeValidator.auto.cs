
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Promotions
{
    public partial class PromoCodeValidator  : DbEntityValidatorBase<PromoCode>
		
    {
        public PromoCodeValidator()
        {
			this.RuleFor(o => o.Code).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(PromoCodeMetadataConstants.Code_MinLength, PromoCodeMetadataConstants.Code_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

