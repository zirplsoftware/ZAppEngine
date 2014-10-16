using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Catalog
{
    public partial class CategoryMapping : CoreEntityMappingBase<Category, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(CategoryMetadataConstants.Name_IsRequired).HasMaxLength(CategoryMetadataConstants.Name_MaxLength, CategoryMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.SeoId).IsRequired(CategoryMetadataConstants.SeoId_IsRequired).HasMaxLength(CategoryMetadataConstants.SeoId_MaxLength, CategoryMetadataConstants.SeoId_IsMaxLength);
			this.Property(o => o.Description).IsRequired(CategoryMetadataConstants.Description_IsRequired).HasMaxLength(CategoryMetadataConstants.Description_MaxLength, CategoryMetadataConstants.Description_IsMaxLength);

            this.HasNavigationProperty(o => o.Parent,
                                        o => o.ParentId,
                                        CategoryMetadataConstants.Parent_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
