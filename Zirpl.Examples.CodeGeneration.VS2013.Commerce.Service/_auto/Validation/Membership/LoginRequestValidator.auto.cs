
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership
{
    public partial class LoginRequestValidator  : AbstractValidator<LoginRequest>
		
    {
        public LoginRequestValidator()
        {
			this.RuleFor(o => o.EmailAddress).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(LoginRequestMetadataConstants.EmailAddress_MinLength, LoginRequestMetadataConstants.EmailAddress_MaxLength);
			this.RuleFor(o => o.Password).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(LoginRequestMetadataConstants.Password_MinLength, LoginRequestMetadataConstants.Password_MaxLength);
			this.RuleFor(o => o.RememberMe).NotNull();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

