using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders
{
    public partial class StripeCustomerChargeOptionMetadataConstants : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Customers.CustomerChargeOptionMetadataConstants
    {
        public const string StripeCustomerId_Name = "StripeCustomerId";
		public const bool StripeCustomerId_IsRequired = true;
		public const bool StripeCustomerId_IsMaxLength = false;
        public const int StripeCustomerId_MinLength = 1;
		public const int StripeCustomerId_MaxLength = 64;

        public const string Last4OfCreditCard_Name = "Last4OfCreditCard";
		public const bool Last4OfCreditCard_IsRequired = true;
		public const bool Last4OfCreditCard_IsMaxLength = false;
        public const int Last4OfCreditCard_MinLength = 1;
		public const int Last4OfCreditCard_MaxLength = 4;

        public const string ExpirationMonthOfCreditCard_Name = "ExpirationMonthOfCreditCard";
		public const bool ExpirationMonthOfCreditCard_IsRequired = true;
		public const bool ExpirationMonthOfCreditCard_IsMaxLength = false;
        public const int ExpirationMonthOfCreditCard_MinLength = 1;
		public const int ExpirationMonthOfCreditCard_MaxLength = 2;

        public const string ExpirationYearOfCreditCard_Name = "ExpirationYearOfCreditCard";
		public const bool ExpirationYearOfCreditCard_IsRequired = true;
		public const bool ExpirationYearOfCreditCard_IsMaxLength = false;
        public const int ExpirationYearOfCreditCard_MinLength = 1;
		public const int ExpirationYearOfCreditCard_MaxLength = 4;

        public const string CreditCardFingerPrint_Name = "CreditCardFingerPrint";
		public const bool CreditCardFingerPrint_IsRequired = false;
		public const bool CreditCardFingerPrint_IsMaxLength = false;
        public const int CreditCardFingerPrint_MinLength = 0;
		public const int CreditCardFingerPrint_MaxLength = 64;

        public const string BillingAddress_Name = "BillingAddress";
		public const bool BillingAddress_IsRequired = true;

		public const string BillingAddressId_Name = "BillingAddressId";
		public const bool BillingAddressId_IsRequired = true;

	}
}
