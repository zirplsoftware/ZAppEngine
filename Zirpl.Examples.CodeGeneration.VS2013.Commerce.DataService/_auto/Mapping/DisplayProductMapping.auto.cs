using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class DisplayProductMapping : CoreEntityMappingBase<DisplayProduct, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(DisplayProductMetadataConstants.Name_IsRequired).HasMaxLength(DisplayProductMetadataConstants.Name_MaxLength, DisplayProductMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.SeoId).IsRequired(DisplayProductMetadataConstants.SeoId_IsRequired).HasMaxLength(DisplayProductMetadataConstants.SeoId_MaxLength, DisplayProductMetadataConstants.SeoId_IsMaxLength);
			this.Property(o => o.Description).IsRequired(DisplayProductMetadataConstants.Description_IsRequired).HasMaxLength(DisplayProductMetadataConstants.Description_MaxLength, DisplayProductMetadataConstants.Description_IsMaxLength);
			this.Property(o => o.Sku).IsRequired(DisplayProductMetadataConstants.Sku_IsRequired).HasMaxLength(DisplayProductMetadataConstants.Sku_MaxLength, DisplayProductMetadataConstants.Sku_IsMaxLength);
			this.Property(o => o.AdminComment).IsRequired(DisplayProductMetadataConstants.AdminComment_IsRequired).HasMaxLength(DisplayProductMetadataConstants.AdminComment_MaxLength, DisplayProductMetadataConstants.AdminComment_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
