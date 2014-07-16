
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class StripeChargeValidator  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders.ChargeValidator<StripeCharge>
		
    {
        public StripeChargeValidator()
        {
			this.RuleFor(o => o.StripeChargeId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeChargeMetadata.StripeChargeId_MinLength, StripeChargeMetadata.StripeChargeId_MaxLength);
			this.RuleFor(o => o.StripeFee).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(StripeChargeMetadata.StripeFee_MinValue, StripeChargeMetadata.StripeFee_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

