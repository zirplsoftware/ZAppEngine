﻿
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class StripeChargeValidator  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders.ChargeValidator<StripeCharge>
		
    {
        public StripeChargeValidator()
        {
			this.RuleFor(o => o.StripeChargeId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeChargeMetadataConstants.StripeChargeId_MinLength, StripeChargeMetadataConstants.StripeChargeId_MaxLength);
			this.RuleFor(o => o.StripeFee).Cascade(CascadeMode.StopOnFirstFailure).NotNull().InclusiveBetween(StripeChargeMetadataConstants.StripeFee_MinValue, StripeChargeMetadataConstants.StripeFee_MaxValue);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

