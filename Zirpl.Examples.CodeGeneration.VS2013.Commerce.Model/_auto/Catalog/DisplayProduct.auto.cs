using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class DisplayProduct  : EntityBase<int>, ICustomizable<DisplayProduct, DisplayProductCustomFieldValue, int>
    {
		public DisplayProduct()
		{
			this.ApplicableDiscounts = this.ApplicableDiscounts ?? new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount>();
			this.CustomFieldValues = this.CustomFieldValues ?? new List<DisplayProductCustomFieldValue>();
		}
		
		public virtual string Name { get; set; }
		public virtual string SeoId { get; set; }
		public virtual string Description { get; set; }
		public virtual string Sku { get; set; }
		public virtual string AdminComment { get; set; }
		public virtual IList<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount> ApplicableDiscounts { get; set; }

		#region CustomFields
		public virtual IList<DisplayProductCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<DisplayProductCustomFieldValue>().ToList();
		}
		#endregion
    }
}

