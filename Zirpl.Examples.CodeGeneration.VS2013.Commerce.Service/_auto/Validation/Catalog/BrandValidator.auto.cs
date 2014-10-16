
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class BrandValidator  : DbEntityValidatorBase<Brand>
		
    {
        public BrandValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(BrandMetadataConstants.Name_MinLength, BrandMetadataConstants.Name_MaxLength);
			this.RuleFor(o => o.SeoId).Length(BrandMetadataConstants.SeoId_MinLength, BrandMetadataConstants.SeoId_MaxLength);
			this.RuleFor(o => o.Description).Length(BrandMetadataConstants.Description_MinLength, BrandMetadataConstants.Description_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

