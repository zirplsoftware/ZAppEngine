
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership
{
    public partial class PasswordResetLinkValidator  : DbEntityValidatorBase<PasswordResetLink>
		
    {
        public PasswordResetLinkValidator()
        {
            this.ForeignEntityNotNullAndIdMatches(o => o.User, o => o.UserId,
                PasswordResetLinkMetadataConstants.User_Name, PasswordResetLinkMetadataConstants.UserId_Name);
			this.RuleFor(o => o.Token).NotEmpty();
			this.RuleFor(o => o.Expires).NotEmpty();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

