
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
			this.RuleFor(o => o.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(TagMetadataConstants.Name_MinLength, TagMetadataConstants.Name_MaxLength);
			this.RuleFor(o => o.SeoId).Length(TagMetadataConstants.SeoId_MinLength, TagMetadataConstants.SeoId_MaxLength);
			this.RuleFor(o => o.Description).Length(TagMetadataConstants.Description_MinLength, TagMetadataConstants.Description_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

