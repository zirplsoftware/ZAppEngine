using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers
{
    public partial class CustomerReferral  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Referral
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.Customer ReferringCustomer { get; set; }
		public virtual int ReferringCustomerId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount ReferringCustomerDiscountAward { get; set; }
		public virtual int? ReferringCustomerDiscountAwardId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsage ReferringCustomerDiscountAwardUsage { get; set; }
		public virtual int? ReferringCustomerDiscountAwardUsageId { get; set; }
    }
}

