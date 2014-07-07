using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions
{
    public abstract partial class Referral : AuditableBase<int>
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.PromoCode PromoCode { get; set; }
		public virtual int PromoCodeId { get; set; }
		public virtual DateTime? ReferredCustomerJoinedDate { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.Customer ReferredCustomer { get; set; }
		public virtual int? ReferredCustomerId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount ReferredCustomerAwardDiscount { get; set; }
		public virtual int? ReferredCustomerAwardDiscountId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsage ReferredCustomerAwardDiscountUsage { get; set; }
		public virtual int? ReferredCustomerAwardDiscountUsageId { get; set; }
    }
}

