using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Settings;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings
{
    public partial class TaxRule  : EntityBase<int>, ICustomizable<TaxRule, TaxRuleCustomFieldValue, int>
    {
		public TaxRule()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<TaxRuleCustomFieldValue>();
		}
		
		public virtual decimal Rate { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Settings.StateProvinceType StateProvinceType { get; set; }
		public virtual int StateProvinceTypeId { get; set; }

		#region CustomFields
		public virtual IList<TaxRuleCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<TaxRuleCustomFieldValue>().ToList();
		}
		#endregion
    }
}

