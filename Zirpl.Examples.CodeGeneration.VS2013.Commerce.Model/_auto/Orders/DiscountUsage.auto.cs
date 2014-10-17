using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class DiscountUsage  : AuditableBase<int>
    {
		public virtual DateTime DateUsed { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions.Discount Discount { get; set; }
		public virtual int DiscountId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.Order Order { get; set; }
		public virtual int OrderId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderItem OrderItem { get; set; }
		public virtual int? OrderItemId { get; set; }
    }
}

