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

			this.Property(o => o.Name).IsRequired(BrandMetadata.Name_IsRequired).HasMaxLength(BrandMetadata.Name_MaxLength, BrandMetadata.Name_IsMaxLength);
			this.Property(o => o.SeoId).IsRequired(BrandMetadata.SeoId_IsRequired).HasMaxLength(BrandMetadata.SeoId_MaxLength, BrandMetadata.SeoId_IsMaxLength);
			this.Property(o => o.Description).IsRequired(BrandMetadata.Description_IsRequired).HasMaxLength(BrandMetadata.Description_MaxLength, BrandMetadata.Description_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
