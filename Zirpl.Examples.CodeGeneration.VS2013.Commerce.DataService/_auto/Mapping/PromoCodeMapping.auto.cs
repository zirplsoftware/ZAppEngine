using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Promotions;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Promotions
{
    public partial class PromoCodeMapping : CoreEntityMappingBase<PromoCode, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Code).IsRequired(PromoCodeMetadata.Code_IsRequired).HasMaxLength(PromoCodeMetadata.Code_MaxLength, PromoCodeMetadata.Code_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
