using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class OrderStatusTypeMapping : DictionaryEntityMapping<OrderStatusType, byte, OrderStatusTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(OrderStatusTypeMetadataConstants.Name_IsRequired).HasMaxLength(OrderStatusTypeMetadataConstants.Name_MaxLength, OrderStatusTypeMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.CustomerFacingName).IsRequired(OrderStatusTypeMetadataConstants.CustomerFacingName_IsRequired).HasMaxLength(OrderStatusTypeMetadataConstants.CustomerFacingName_MaxLength, OrderStatusTypeMetadataConstants.CustomerFacingName_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
