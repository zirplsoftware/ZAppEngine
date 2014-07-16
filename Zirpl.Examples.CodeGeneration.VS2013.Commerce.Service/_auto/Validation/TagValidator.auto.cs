
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation
{
    public partial class TagValidator  : DbEntityValidatorBase<Tag>
		
    {
        public TagValidator()
        {
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(TagMetadata.Name_MinLength, TagMetadata.Name_MaxLength);
			this.RuleFor(o => o.SeoId).Length(TagMetadata.SeoId_MinLength, TagMetadata.SeoId_MaxLength);
			this.RuleFor(o => o.Description).Length(TagMetadata.Description_MinLength, TagMetadata.Description_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

