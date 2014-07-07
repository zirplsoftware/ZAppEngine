using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions
{
    public partial class Discount : AuditableBase<int>
    {
		public Discount()
		{
			if (this.AppliesToDisplayProducts == null)
			{
				this.AppliesToDisplayProducts = new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.DisplayProduct>();
			}
		}

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
    }
}

