using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class OrderMapping : EntityMappingBase<Order, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Date).IsRequired(OrderMetadataConstants.Date_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.OrderChargeStatusType,
                                        o => o.OrderChargeStatusTypeId,
                                        OrderMetadataConstants.OrderChargeStatusType_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.OrderStatusType,
                                        o => o.OrderStatusTypeId,
                                        OrderMetadataConstants.OrderStatusType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.SubtotalAmountBeforeDiscount).IsRequired(OrderMetadataConstants.SubtotalAmountBeforeDiscount_IsRequired).IsCurrency();
			this.Property(o => o.OriginalSubtotalAmount).IsRequired(OrderMetadataConstants.OriginalSubtotalAmount_IsRequired).IsCurrency();
			this.Property(o => o.OriginalTaxAmount).IsRequired(OrderMetadataConstants.OriginalTaxAmount_IsRequired).IsCurrency();
			this.Property(o => o.OriginalTotalAmount).IsRequired(OrderMetadataConstants.OriginalTotalAmount_IsRequired).IsCurrency();

            this.HasNavigationProperty(o => o.ShippingAddress,
                                        o => o.ShippingAddressId,
                                        OrderMetadataConstants.ShippingAddress_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Customer,
                                        o => o.CustomerId,
                                        OrderMetadataConstants.Customer_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.CustomerChargeOption,
                                        o => o.CustomerChargeOptionId,
                                        OrderMetadataConstants.CustomerChargeOption_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
