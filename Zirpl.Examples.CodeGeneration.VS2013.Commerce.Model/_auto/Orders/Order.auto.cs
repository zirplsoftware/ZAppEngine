using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders
{
    public partial class Order  : AuditableBase<int>, ICustomizable<Order, OrderCustomFieldValue, int>
    {
		public Order()
		{
			this.Charges = this.Charges ?? new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.Charge>();
			this.DiscountUsages = this.DiscountUsages ?? new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsage>();
			this.OrderItems = this.OrderItems ?? new List<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderItem>();
			this.CustomFieldValues = this.CustomFieldValues ?? new List<OrderCustomFieldValue>();
		}
		
		public virtual DateTime Date { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderChargeStatusType OrderChargeStatusType { get; set; }
		public virtual byte OrderChargeStatusTypeId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderStatusType OrderStatusType { get; set; }
		public virtual byte OrderStatusTypeId { get; set; }
		public virtual decimal SubtotalAmountBeforeDiscount { get; set; }
		public virtual decimal OriginalSubtotalAmount { get; set; }
		public virtual decimal OriginalTaxAmount { get; set; }
		public virtual decimal OriginalTotalAmount { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Address ShippingAddress { get; set; }
		public virtual int ShippingAddressId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.Customer Customer { get; set; }
		public virtual int CustomerId { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customers.CustomerChargeOption CustomerChargeOption { get; set; }
		public virtual int CustomerChargeOptionId { get; set; }
		public virtual IList<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.Charge> Charges { get; set; }
		public virtual IList<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.DiscountUsage> DiscountUsages { get; set; }
		public virtual IList<Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders.OrderItem> OrderItems { get; set; }


		public virtual IList<OrderCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<OrderCustomFieldValue>().ToList();
		}
    }
}

