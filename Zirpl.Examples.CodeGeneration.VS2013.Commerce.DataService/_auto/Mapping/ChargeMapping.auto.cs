using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class ChargeMapping : CoreEntityMappingBase<Charge, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Date).IsRequired(ChargeMetadataConstants.Date_IsRequired).IsDateTime();
			this.Property(o => o.Amount).IsRequired(ChargeMetadataConstants.Amount_IsRequired).IsCurrency();

            this.HasNavigationProperty(o => o.ChargeType,
                                        o => o.ChargeTypeId,
                                        ChargeMetadataConstants.ChargeType_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ChargeMethodType,
                                        o => o.ChargeMethodTypeId,
                                        ChargeMetadataConstants.ChargeMethodType_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.ChargeStatusType,
                                        o => o.ChargeStatusTypeId,
                                        ChargeMetadataConstants.ChargeStatusType_IsRequired,
                                        CascadeOnDeleteOption.No);

            this.HasNavigationProperty(o => o.Order,
                                        o => o.OrderId,
                                        ChargeMetadataConstants.Order_IsRequired,
                                        CascadeOnDeleteOption.No,
										o => o.Charges);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
