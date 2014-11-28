
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class CategoryValidator  : DbEntityValidatorBase<Category>
		
    {
        public CategoryValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(CategoryMetadataConstants.Name_MinLength, CategoryMetadataConstants.Name_MaxLength);
			this.RuleFor(o => o.SeoId).Length(CategoryMetadataConstants.SeoId_MinLength, CategoryMetadataConstants.SeoId_MaxLength);
			this.RuleFor(o => o.Description).Length(CategoryMetadataConstants.Description_MinLength, CategoryMetadataConstants.Description_MaxLength);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.Parent, o => o.ParentId,
                CategoryMetadataConstants.Parent_Name, CategoryMetadataConstants.ParentId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

