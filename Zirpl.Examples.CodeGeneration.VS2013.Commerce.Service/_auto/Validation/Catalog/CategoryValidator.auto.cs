
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Catalog
{
    public partial class CategoryValidator  : DbEntityValidatorBase<Category>
		
    {
        public CategoryValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(CategoryMetadata.Name_MinLength, CategoryMetadata.Name_MaxLength);
			this.RuleFor(o => o.SeoId).Length(CategoryMetadata.SeoId_MinLength, CategoryMetadata.SeoId_MaxLength);
			this.RuleFor(o => o.Description).Length(CategoryMetadata.Description_MinLength, CategoryMetadata.Description_MaxLength);
            this.ForeignEntityAndIdMatchIfNotNull(o => o.Parent, o => o.ParentId,
                CategoryMetadata.Parent_Name, CategoryMetadata.ParentId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

