using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions
{
    public partial class DiscountAmountTypeMapping : DictionaryEntityMapping<DiscountAmountType, byte, DiscountAmountTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(DiscountAmountTypeMetadataConstants.Name_IsRequired).HasMaxLength(DiscountAmountTypeMetadataConstants.Name_MaxLength, DiscountAmountTypeMetadataConstants.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
