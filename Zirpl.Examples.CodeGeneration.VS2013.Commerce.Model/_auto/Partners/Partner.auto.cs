using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class Partner  : AuditableBase<int>, ICustomizable<Partner, PartnerCustomFieldValue, int>
    {
		public Partner()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<PartnerCustomFieldValue>();
		}
		
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.Visitor Visitor { get; set; }
		public virtual int VisitorId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Address Address { get; set; }
		public virtual int AddressId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferralPlan ReferralPlan { get; set; }
		public virtual int? ReferralPlanId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.PromoCode PromoCode { get; set; }
		public virtual int PromoCodeId { get; set; }
		public virtual string CrmUrl { get; set; }


		public virtual IList<PartnerCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<PartnerCustomFieldValue>().ToList();
		}
    }
}

