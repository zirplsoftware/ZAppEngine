
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers
{
    public partial class CustomerRegistrationRequestValidator  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership.UserRegistrationRequestBaseValidator<CustomerRegistrationRequest>
		
    {
        public CustomerRegistrationRequestValidator()
        {
			this.RuleFor(o => o.AcceptTermsOfUse).NotNull();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

