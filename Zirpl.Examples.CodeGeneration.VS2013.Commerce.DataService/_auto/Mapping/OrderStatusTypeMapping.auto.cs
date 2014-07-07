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

			this.Property(o => o.Name).IsRequired(OrderStatusTypeMetadata.Name_IsRequired).HasMaxLength(OrderStatusTypeMetadata.Name_MaxLength, OrderStatusTypeMetadata.Name_IsMaxLength);
			this.Property(o => o.CustomerFacingName).IsRequired(OrderStatusTypeMetadata.CustomerFacingName_IsRequired).HasMaxLength(OrderStatusTypeMetadata.CustomerFacingName_MaxLength, OrderStatusTypeMetadata.CustomerFacingName_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
