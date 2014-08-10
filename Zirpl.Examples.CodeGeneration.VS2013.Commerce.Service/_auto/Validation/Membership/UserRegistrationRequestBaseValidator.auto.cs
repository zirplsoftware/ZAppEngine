
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership
{
    public abstract partial class UserRegistrationRequestBaseValidator<T>  : AbstractValidator<UserRegistrationRequestBase>
		where T : UserRegistrationRequestBase
    {
        protected UserRegistrationRequestBaseValidator()
        {
			this.RuleFor(o => o.EmailAddress).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(UserRegistrationRequestBaseMetadata.EmailAddress_MinLength, UserRegistrationRequestBaseMetadata.EmailAddress_MaxLength);
			this.RuleFor(o => o.Password).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(UserRegistrationRequestBaseMetadata.Password_MinLength, UserRegistrationRequestBaseMetadata.Password_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

