using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions
{
    public partial class Discount  : EntityBase<int>, ICustomizable<Discount, DiscountCustomFieldValue, int>
    {
		public Discount()
		{
			this.AppliesToDisplayProducts = this.AppliesToDisplayProducts ?? new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.DisplayProduct>();
			this.CustomFieldValues = this.CustomFieldValues ?? new List<DiscountCustomFieldValue>();
		}
		
		public virtual string Name { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.PromoCode PromoCode { get; set; }
		public virtual int PromoCodeId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.DiscountApplicabilityType DiscountApplicabilityType { get; set; }
		public virtual byte DiscountApplicabilityTypeId { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.DiscountAmountType DiscountAmountType { get; set; }
		public virtual byte DiscountAmountTypeId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsageRestrictionType DiscountUsageRestrictionType { get; set; }
		public virtual byte DiscountUsageRestrictionTypeId { get; set; }
		public virtual int? DiscountUsageRestrictionQuantity { get; set; }
		public virtual int? StopAfterChargeCyles { get; set; }
		public virtual DateTime? StartDate { get; set; }
		public virtual DateTime? EndDate { get; set; }
		public virtual bool Published { get; set; }
		public virtual IList<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.DisplayProduct> AppliesToDisplayProducts { get; set; }

		#region CustomFields
		public virtual IList<DiscountCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<DiscountCustomFieldValue>().ToList();
		}
		#endregion
    }
}

