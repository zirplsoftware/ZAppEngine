using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class OrderItem  : AuditableBase<int>, ICustomizable<OrderItem, OrderItemCustomFieldValue, int>
    {
		public OrderItem()
		{
			this.DiscountUsages = this.DiscountUsages ?? new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsage>();
			this.CustomFieldValues = this.CustomFieldValues ?? new List<OrderItemCustomFieldValue>();
		}
		
		public virtual int Quantity { get; set; }
		public virtual string ItemName { get; set; }
		public virtual decimal ItemAmountBeforeDiscount { get; set; }
		public virtual decimal OriginalItemAmount { get; set; }
		public virtual bool Cancelled { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.Order Order { get; set; }
		public virtual int OrderId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.DisplayProduct DisplayProduct { get; set; }
		public virtual int DisplayProductId { get; set; }
		public virtual IList<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsage> DiscountUsages { get; set; }


		public virtual IList<OrderItemCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<OrderItemCustomFieldValue>().ToList();
		}
    }
}

