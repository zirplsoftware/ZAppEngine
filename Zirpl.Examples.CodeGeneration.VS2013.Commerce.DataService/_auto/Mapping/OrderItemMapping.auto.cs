using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class OrderItemMapping : CoreEntityMappingBase<OrderItem, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Quantity).IsRequired(OrderItemMetadata.Quantity_IsRequired);
			this.Property(o => o.ItemName).IsRequired(OrderItemMetadata.ItemName_IsRequired).HasMaxLength(OrderItemMetadata.ItemName_MaxLength, OrderItemMetadata.ItemName_IsMaxLength);
			this.Property(o => o.ItemAmountBeforeDiscount).IsRequired(OrderItemMetadata.ItemAmountBeforeDiscount_IsRequired).IsCurrency();
			this.Property(o => o.OriginalItemAmount).IsRequired(OrderItemMetadata.OriginalItemAmount_IsRequired).IsCurrency();
			this.Property(o => o.Cancelled).IsRequired(OrderItemMetadata.Cancelled_IsRequired);

            this.HasNavigationProperty(o => o.Order,
                                        o => o.OrderId,
                                        OrderItemMetadata.Order_IsRequired,
                                        CascadeOnDeleteOption.No,
										o => o.OrderItems);

            this.HasNavigationProperty(o => o.DisplayProduct,
                                        o => o.DisplayProductId,
                                        OrderItemMetadata.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
