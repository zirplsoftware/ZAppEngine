using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class StripeCustomerChargeOption  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.CustomerChargeOption
    {
		public virtual string StripeCustomerId { get; set; }
		public virtual string Last4OfCreditCard { get; set; }
		public virtual string ExpirationMonthOfCreditCard { get; set; }
		public virtual string ExpirationYearOfCreditCard { get; set; }
		public virtual string CreditCardFingerPrint { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Address BillingAddress { get; set; }
		public virtual int BillingAddressId { get; set; }
    }
}

