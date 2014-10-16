using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class BrandMapping : CoreEntityMappingBase<Brand, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(BrandMetadataConstants.Name_IsRequired).HasMaxLength(BrandMetadataConstants.Name_MaxLength, BrandMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.SeoId).IsRequired(BrandMetadataConstants.SeoId_IsRequired).HasMaxLength(BrandMetadataConstants.SeoId_MaxLength, BrandMetadataConstants.SeoId_IsMaxLength);
			this.Property(o => o.Description).IsRequired(BrandMetadataConstants.Description_IsRequired).HasMaxLength(BrandMetadataConstants.Description_MaxLength, BrandMetadataConstants.Description_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
