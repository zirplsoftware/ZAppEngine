
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Membership;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership
{
    public partial class SupportUserRegistrationRequestValidator  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership.UserRegistrationRequestBaseValidator<SupportUserRegistrationRequest>
		
    {
        public SupportUserRegistrationRequestValidator()
        {

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

