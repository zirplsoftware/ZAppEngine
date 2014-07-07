using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions
{
    public partial class DiscountApplicabilityTypeMapping : DictionaryEntityMapping<DiscountApplicabilityType, byte, DiscountApplicabilityTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(DiscountApplicabilityTypeMetadata.Name_IsRequired).HasMaxLength(DiscountApplicabilityTypeMetadata.Name_MaxLength, DiscountApplicabilityTypeMetadata.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
