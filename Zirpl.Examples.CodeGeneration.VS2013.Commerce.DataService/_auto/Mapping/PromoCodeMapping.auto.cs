using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions
{
    public partial class PromoCodeMapping : CoreEntityMappingBase<PromoCode, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Code).IsRequired(PromoCodeMetadataConstants.Code_IsRequired).HasMaxLength(PromoCodeMetadataConstants.Code_MaxLength, PromoCodeMetadataConstants.Code_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
