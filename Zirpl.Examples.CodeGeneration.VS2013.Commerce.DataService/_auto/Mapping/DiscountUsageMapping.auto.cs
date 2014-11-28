using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class DiscountUsageMapping : EntityMappingBase<DiscountUsage, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.DateUsed).IsRequired(DiscountUsageMetadataConstants.DateUsed_IsRequired).IsDateTime();

            this.HasNavigationProperty(o => o.Discount,
                                        o => o.DiscountId,
                                        DiscountUsageMetadataConstants.Discount_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Order,
                                        o => o.OrderId,
                                        DiscountUsageMetadataConstants.Order_IsRequired,
                                        CascadeOnDeleteOption.No,
										o => o.DiscountUsages);

            this.HasNavigationProperty(o => o.OrderItem,
                                        o => o.OrderItemId,
                                        DiscountUsageMetadataConstants.OrderItem_IsRequired,
                                        CascadeOnDeleteOption.No,
										o => o.DiscountUsages);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
