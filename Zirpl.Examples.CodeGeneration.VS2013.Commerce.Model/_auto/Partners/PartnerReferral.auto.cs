using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class PartnerReferral  : Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Referral
    {
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferralCouponRequest Request { get; set; }
		public virtual int? RequestId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.Partner Partner { get; set; }
		public virtual int PartnerId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferralPlan Plan { get; set; }
		public virtual int? PlanId { get; set; }
    }
}

