using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model
{
    public partial class Tag  : AuditableBase<int>, ICustomizable<Tag, TagCustomFieldValue, int>
    {
		public Tag()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<TagCustomFieldValue>();
		}
		
		public virtual string Name { get; set; }
		public virtual string SeoId { get; set; }
		public virtual string Description { get; set; }

		#region CustomFields
		public virtual IList<TagCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<TagCustomFieldValue>().ToList();
		}
		#endregion
    }
}

