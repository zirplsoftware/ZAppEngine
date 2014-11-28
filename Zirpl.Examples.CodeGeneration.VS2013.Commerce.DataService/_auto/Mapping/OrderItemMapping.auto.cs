using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class OrderItemMapping : EntityMappingBase<OrderItem, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Quantity).IsRequired(OrderItemMetadataConstants.Quantity_IsRequired);
			this.Property(o => o.ItemName).IsRequired(OrderItemMetadataConstants.ItemName_IsRequired).HasMaxLength(OrderItemMetadataConstants.ItemName_MaxLength, OrderItemMetadataConstants.ItemName_IsMaxLength);
			this.Property(o => o.ItemAmountBeforeDiscount).IsRequired(OrderItemMetadataConstants.ItemAmountBeforeDiscount_IsRequired).IsCurrency();
			this.Property(o => o.OriginalItemAmount).IsRequired(OrderItemMetadataConstants.OriginalItemAmount_IsRequired).IsCurrency();
			this.Property(o => o.Cancelled).IsRequired(OrderItemMetadataConstants.Cancelled_IsRequired);

            this.HasNavigationProperty(o => o.Order,
                                        o => o.OrderId,
                                        OrderItemMetadataConstants.Order_IsRequired,
                                        CascadeOnDeleteOption.No,
										o => o.OrderItems);

            this.HasNavigationProperty(o => o.DisplayProduct,
                                        o => o.DisplayProductId,
                                        OrderItemMetadataConstants.DisplayProduct_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
