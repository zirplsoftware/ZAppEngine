using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class Brand  : AuditableBase<int>, ICustomizable<Brand, BrandCustomFieldValue, int>
    {
		public Brand()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<BrandCustomFieldValue>();
		}
		
		public virtual string Name { get; set; }
		public virtual string SeoId { get; set; }
		public virtual string Description { get; set; }


		public virtual IList<BrandCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<BrandCustomFieldValue>().ToList();
		}
    }
}

