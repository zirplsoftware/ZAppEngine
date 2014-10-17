
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class DisplayProductValidator  : DbEntityValidatorBase<DisplayProduct>
		
    {
        public DisplayProductValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(DisplayProductMetadataConstants.Name_MinLength, DisplayProductMetadataConstants.Name_MaxLength);
			this.RuleFor(o => o.SeoId).Length(DisplayProductMetadataConstants.SeoId_MinLength, DisplayProductMetadataConstants.SeoId_MaxLength);
			this.RuleFor(o => o.Description).Length(DisplayProductMetadataConstants.Description_MinLength, DisplayProductMetadataConstants.Description_MaxLength);
			this.RuleFor(o => o.Sku).Length(DisplayProductMetadataConstants.Sku_MinLength, DisplayProductMetadataConstants.Sku_MaxLength);
			this.RuleFor(o => o.AdminComment).Length(DisplayProductMetadataConstants.AdminComment_MinLength, DisplayProductMetadataConstants.AdminComment_MaxLength);
			// unsure how to follow this for validation or even if it should with EF- Collection property: ApplicableDiscounts

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

