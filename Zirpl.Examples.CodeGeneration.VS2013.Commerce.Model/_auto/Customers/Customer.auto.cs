using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Customers;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers
{
    public partial class Customer  : AuditableBase<int>, ICustomizable<Customer, CustomerCustomFieldValue, int>
    {
		public Customer()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<CustomerCustomFieldValue>();
		}
		
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Membership.Visitor Visitor { get; set; }
		public virtual int VisitorId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.PromoCode PromoCode { get; set; }
		public virtual int PromoCodeId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Address CurrentShippingAddress { get; set; }
		public virtual int? CurrentShippingAddressId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.CustomerChargeOption CurrentCustomerChargeOption { get; set; }
		public virtual int? CurrentCustomerChargeOptionId { get; set; }

		#region CustomFields
		public virtual IList<CustomerCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<CustomerCustomFieldValue>().ToList();
		}
		#endregion
    }
}

