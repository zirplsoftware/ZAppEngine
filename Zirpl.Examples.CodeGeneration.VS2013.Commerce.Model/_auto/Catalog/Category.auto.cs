using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class Category  : EntityBase<int>, ICustomizable<Category, CategoryCustomFieldValue, int>
    {
		public Category()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<CategoryCustomFieldValue>();
		}
		
		public virtual string Name { get; set; }
		public virtual string SeoId { get; set; }
		public virtual string Description { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.Category Parent { get; set; }
		public virtual int? ParentId { get; set; }

		#region CustomFields
		public virtual IList<CategoryCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<CategoryCustomFieldValue>().ToList();
		}
		#endregion
    }
}

