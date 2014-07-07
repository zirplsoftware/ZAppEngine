using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class DisplayProduct : AuditableBase<int>
    {
		public DisplayProduct()
		{
			if (this.ApplicableDiscounts == null)
			{
				this.ApplicableDiscounts = new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount>();
			}
		}

		public virtual string Name { get; set; }
		public virtual string SeoId { get; set; }
		public virtual string Description { get; set; }
		public virtual string Sku { get; set; }
		public virtual string AdminComment { get; set; }
		public virtual IList<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount> ApplicableDiscounts { get; set; }
    }
}

