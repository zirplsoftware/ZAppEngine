using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Orders;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Orders;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Orders
{
    public partial class DiscountUsageRestrictionTypeMapping : DictionaryEntityMapping<DiscountUsageRestrictionType, byte, DiscountUsageRestrictionTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(DiscountUsageRestrictionTypeMetadataConstants.Name_IsRequired).HasMaxLength(DiscountUsageRestrictionTypeMetadataConstants.Name_MaxLength, DiscountUsageRestrictionTypeMetadataConstants.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
