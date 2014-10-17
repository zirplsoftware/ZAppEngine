using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class PartnerReferralPlan  : AuditableBase<int>, ICustomizable<PartnerReferralPlan, PartnerReferralPlanCustomFieldValue, int>
    {
		public PartnerReferralPlan()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<PartnerReferralPlanCustomFieldValue>();
		}
		
		public virtual string Name { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount ReferredCustomerAwardDiscount { get; set; }
		public virtual int ReferredCustomerAwardDiscountId { get; set; }

		#region CustomFields
		public virtual IList<PartnerReferralPlanCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<PartnerReferralPlanCustomFieldValue>().ToList();
		}
		#endregion
    }
}

