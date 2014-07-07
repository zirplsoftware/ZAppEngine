using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class PartnerReferralPlan  : AuditableBase<int>
    {
		public virtual string Name { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount ReferredCustomerAwardDiscount { get; set; }
		public virtual int ReferredCustomerAwardDiscountId { get; set; }
    }
}

