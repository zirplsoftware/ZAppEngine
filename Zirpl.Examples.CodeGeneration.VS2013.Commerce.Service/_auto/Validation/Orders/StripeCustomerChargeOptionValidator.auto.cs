
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Orders
{
    public partial class StripeCustomerChargeOptionValidator  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Customers.CustomerChargeOptionValidator<StripeCustomerChargeOption>
		
    {
        public StripeCustomerChargeOptionValidator()
        {
			this.RuleFor(o => o.StripeCustomerId).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeCustomerChargeOptionMetadataConstants.StripeCustomerId_MinLength, StripeCustomerChargeOptionMetadataConstants.StripeCustomerId_MaxLength);
			this.RuleFor(o => o.Last4OfCreditCard).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeCustomerChargeOptionMetadataConstants.Last4OfCreditCard_MinLength, StripeCustomerChargeOptionMetadataConstants.Last4OfCreditCard_MaxLength);
			this.RuleFor(o => o.ExpirationMonthOfCreditCard).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeCustomerChargeOptionMetadataConstants.ExpirationMonthOfCreditCard_MinLength, StripeCustomerChargeOptionMetadataConstants.ExpirationMonthOfCreditCard_MaxLength);
			this.RuleFor(o => o.ExpirationYearOfCreditCard).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(StripeCustomerChargeOptionMetadataConstants.ExpirationYearOfCreditCard_MinLength, StripeCustomerChargeOptionMetadataConstants.ExpirationYearOfCreditCard_MaxLength);
			this.RuleFor(o => o.CreditCardFingerPrint).Length(StripeCustomerChargeOptionMetadataConstants.CreditCardFingerPrint_MinLength, StripeCustomerChargeOptionMetadataConstants.CreditCardFingerPrint_MaxLength);
            this.ForeignEntityNotNullAndIdMatches(o => o.BillingAddress, o => o.BillingAddressId,
                StripeCustomerChargeOptionMetadataConstants.BillingAddress_Name, StripeCustomerChargeOptionMetadataConstants.BillingAddressId_Name);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

