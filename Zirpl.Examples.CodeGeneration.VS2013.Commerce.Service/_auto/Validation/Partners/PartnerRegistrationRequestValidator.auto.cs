
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Partners
{
    public partial class PartnerRegistrationRequestValidator  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Membership.UserRegistrationRequestBaseValidator<PartnerRegistrationRequest>
		
    {
        public PartnerRegistrationRequestValidator()
        {
			this.RuleFor(o => o.AcceptTermsOfUse).NotNull();

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

