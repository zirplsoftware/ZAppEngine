
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class DisplayProductValidator  : DbEntityValidatorBase<DisplayProduct>
		
    {
        public DisplayProductValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(DisplayProductMetadata.Name_MinLength, DisplayProductMetadata.Name_MaxLength);
			this.RuleFor(o => o.SeoId).Length(DisplayProductMetadata.SeoId_MinLength, DisplayProductMetadata.SeoId_MaxLength);
			this.RuleFor(o => o.Description).Length(DisplayProductMetadata.Description_MinLength, DisplayProductMetadata.Description_MaxLength);
			this.RuleFor(o => o.Sku).Length(DisplayProductMetadata.Sku_MinLength, DisplayProductMetadata.Sku_MaxLength);
			this.RuleFor(o => o.AdminComment).Length(DisplayProductMetadata.AdminComment_MinLength, DisplayProductMetadata.AdminComment_MaxLength);
			// unsure how to follow this for validation or even if it should with EF- Collection property: ApplicableDiscounts

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

