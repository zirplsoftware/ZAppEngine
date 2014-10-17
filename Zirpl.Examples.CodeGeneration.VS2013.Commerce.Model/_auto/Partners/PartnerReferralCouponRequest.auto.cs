using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Partners;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners
{
    public partial class PartnerReferralCouponRequest  : AuditableBase<int>, ICustomizable<PartnerReferralCouponRequest, PartnerReferralCouponRequestCustomFieldValue, int>
    {
		public PartnerReferralCouponRequest()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<PartnerReferralCouponRequestCustomFieldValue>();
		}
		
		public virtual DateTime RequestDate { get; set; }
		public virtual int Quantity { get; set; }
		public virtual DateTime? ShippedDate { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.Partner Partner { get; set; }
		public virtual int PartnerId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Partners.PartnerReferralCouponRequestStatusType PartnerReferralCouponRequestStatusType { get; set; }
		public virtual byte PartnerReferralCouponRequestStatusTypeId { get; set; }

		#region CustomFields
		public virtual IList<PartnerReferralCouponRequestCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<PartnerReferralCouponRequestCustomFieldValue>().ToList();
		}
		#endregion
    }
}

