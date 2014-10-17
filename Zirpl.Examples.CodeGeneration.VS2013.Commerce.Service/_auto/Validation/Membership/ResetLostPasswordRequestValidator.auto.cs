
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership
{
    public partial class ResetLostPasswordRequestValidator  : AbstractValidator<ResetLostPasswordRequest>
		
    {
        public ResetLostPasswordRequestValidator()
        {
			this.RuleFor(o => o.NewPassword).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(ResetLostPasswordRequestMetadataConstants.NewPassword_MinLength, ResetLostPasswordRequestMetadataConstants.NewPassword_MaxLength);
			this.RuleFor(o => o.UserId).NotEmpty();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

