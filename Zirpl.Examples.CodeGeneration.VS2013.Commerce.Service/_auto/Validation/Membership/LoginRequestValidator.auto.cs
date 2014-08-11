
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership
{
    public partial class LoginRequestValidator  : AbstractValidator<LoginRequest>
		
    {
        public LoginRequestValidator()
        {
			this.RuleFor(o => o.EmailAddress).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(LoginRequestMetadata.EmailAddress_MinLength, LoginRequestMetadata.EmailAddress_MaxLength);
			this.RuleFor(o => o.Password).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(LoginRequestMetadata.Password_MinLength, LoginRequestMetadata.Password_MaxLength);
			this.RuleFor(o => o.RememberMe).NotNull();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

