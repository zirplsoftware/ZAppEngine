using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class Partner : AuditableBase<int>
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.Visitor Visitor { get; set; }
		public virtual int VisitorId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Address Address { get; set; }
		public virtual int AddressId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferralPlan ReferralPlan { get; set; }
		public virtual int? ReferralPlanId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.PromoCode PromoCode { get; set; }
		public virtual int PromoCodeId { get; set; }
		public virtual string CrmUrl { get; set; }
    }
}

