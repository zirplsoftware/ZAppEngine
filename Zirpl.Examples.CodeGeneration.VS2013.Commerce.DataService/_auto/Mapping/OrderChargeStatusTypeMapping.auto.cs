using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class OrderChargeStatusTypeMapping : DictionaryEntityMapping<OrderChargeStatusType, byte, OrderChargeStatusTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(OrderChargeStatusTypeMetadataConstants.Name_IsRequired).HasMaxLength(OrderChargeStatusTypeMetadataConstants.Name_MaxLength, OrderChargeStatusTypeMetadataConstants.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
