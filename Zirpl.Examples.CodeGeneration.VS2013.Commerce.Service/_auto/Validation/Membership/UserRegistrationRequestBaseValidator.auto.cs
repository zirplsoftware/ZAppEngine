
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership
{
    public abstract partial class UserRegistrationRequestBaseValidator<T>  : AbstractValidator<T>
		where T : UserRegistrationRequestBase
    {
        protected UserRegistrationRequestBaseValidator()
        {
			this.RuleFor(o => o.EmailAddress).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(UserRegistrationRequestBaseMetadataConstants.EmailAddress_MinLength, UserRegistrationRequestBaseMetadataConstants.EmailAddress_MaxLength);
			this.RuleFor(o => o.Password).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(UserRegistrationRequestBaseMetadataConstants.Password_MinLength, UserRegistrationRequestBaseMetadataConstants.Password_MaxLength);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

