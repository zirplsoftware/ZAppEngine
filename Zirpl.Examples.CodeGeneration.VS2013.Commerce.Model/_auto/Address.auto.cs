using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model
{
    public partial class Address  : AuditableBase<int>, ICustomizable<Address, AddressCustomFieldValue, int>
    {
		public Address()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<AddressCustomFieldValue>();
		}
		
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.NamePrefixType Prefix { get; set; }
		public virtual byte? PrefixId { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string MiddleName { get; set; }
		public virtual string LastName { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.NameSuffixType Suffix { get; set; }
		public virtual byte? SuffixId { get; set; }
		public virtual string Position { get; set; }
		public virtual string CompanyName { get; set; }
		public virtual string StreetLine1 { get; set; }
		public virtual string StreetLine2 { get; set; }
		public virtual string City { get; set; }
		public virtual string PostalCode { get; set; }
		public virtual string PhoneNumber { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings.StateProvinceType StateProvinceType { get; set; }
		public virtual int? StateProvinceTypeId { get; set; }


		public virtual IList<AddressCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<AddressCustomFieldValue>().ToList();
		}
    }
}

