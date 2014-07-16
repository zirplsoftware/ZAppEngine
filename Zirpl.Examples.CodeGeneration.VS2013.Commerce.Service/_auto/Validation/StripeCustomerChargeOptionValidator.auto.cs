
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class StripeCustomerChargeOptionValidator  : CustomerChargeOptionValidatorStripeCustomerChargeOption
		
    {
        public StripeCustomerChargeOptionValidator()
        {
			this.RuleFor(o => o.StripeCustomerId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeCustomerChargeOptionMetadata.StripeCustomerId_MinLength, StripeCustomerChargeOptionMetadata.StripeCustomerId_MaxLength);
			this.RuleFor(o => o.Last4OfCreditCard).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeCustomerChargeOptionMetadata.Last4OfCreditCard_MinLength, StripeCustomerChargeOptionMetadata.Last4OfCreditCard_MaxLength);
			this.RuleFor(o => o.ExpirationMonthOfCreditCard).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeCustomerChargeOptionMetadata.ExpirationMonthOfCreditCard_MinLength, StripeCustomerChargeOptionMetadata.ExpirationMonthOfCreditCard_MaxLength);
			this.RuleFor(o => o.ExpirationYearOfCreditCard).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeCustomerChargeOptionMetadata.ExpirationYearOfCreditCard_MinLength, StripeCustomerChargeOptionMetadata.ExpirationYearOfCreditCard_MaxLength);
			this.RuleFor(o => o.CreditCardFingerPrint).Length(StripeCustomerChargeOptionMetadata.CreditCardFingerPrint_MinLength, StripeCustomerChargeOptionMetadata.CreditCardFingerPrint_MaxLength);
            this.ForeignEntityNotNullAndIdMatches(o => o.BillingAddress, o => o.BillingAddressId,
                StripeCustomerChargeOptionMetadata.BillingAddress_Name, StripeCustomerChargeOptionMetadata.BillingAddressId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

