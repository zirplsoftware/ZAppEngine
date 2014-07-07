using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class OrderMapping : CoreEntityMappingBase<Order, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Date).IsRequired(OrderMetadata.Date_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.OrderChargeStatusType,
                                        o => o.OrderChargeStatusTypeId,
                                        OrderMetadata.OrderChargeStatusType_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.OrderStatusType,
                                        o => o.OrderStatusTypeId,
                                        OrderMetadata.OrderStatusType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.SubtotalAmountBeforeDiscount).IsRequired(OrderMetadata.SubtotalAmountBeforeDiscount_IsRequired).IsCurrency();
			this.Property(o => o.OriginalSubtotalAmount).IsRequired(OrderMetadata.OriginalSubtotalAmount_IsRequired).IsCurrency();
			this.Property(o => o.OriginalTaxAmount).IsRequired(OrderMetadata.OriginalTaxAmount_IsRequired).IsCurrency();
			this.Property(o => o.OriginalTotalAmount).IsRequired(OrderMetadata.OriginalTotalAmount_IsRequired).IsCurrency();

            this.HasNavigationProperty(o => o.ShippingAddress,
                                        o => o.ShippingAddressId,
                                        OrderMetadata.ShippingAddress_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Customer,
                                        o => o.CustomerId,
                                        OrderMetadata.Customer_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CustomerChargeOption,
                                        o => o.CustomerChargeOptionId,
                                        OrderMetadata.CustomerChargeOption_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
