using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class DisplayProductMapping : CoreEntityMappingBase<DisplayProduct, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(DisplayProductMetadata.Name_IsRequired).HasMaxLength(DisplayProductMetadata.Name_MaxLength, DisplayProductMetadata.Name_IsMaxLength);
			this.Property(o => o.SeoId).IsRequired(DisplayProductMetadata.SeoId_IsRequired).HasMaxLength(DisplayProductMetadata.SeoId_MaxLength, DisplayProductMetadata.SeoId_IsMaxLength);
			this.Property(o => o.Description).IsRequired(DisplayProductMetadata.Description_IsRequired).HasMaxLength(DisplayProductMetadata.Description_MaxLength, DisplayProductMetadata.Description_IsMaxLength);
			this.Property(o => o.Sku).IsRequired(DisplayProductMetadata.Sku_IsRequired).HasMaxLength(DisplayProductMetadata.Sku_MaxLength, DisplayProductMetadata.Sku_IsMaxLength);
			this.Property(o => o.AdminComment).IsRequired(DisplayProductMetadata.AdminComment_IsRequired).HasMaxLength(DisplayProductMetadata.AdminComment_MaxLength, DisplayProductMetadata.AdminComment_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
