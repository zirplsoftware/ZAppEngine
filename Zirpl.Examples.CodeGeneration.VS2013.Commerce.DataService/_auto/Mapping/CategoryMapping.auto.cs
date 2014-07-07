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

			this.Property(o => o.Name).IsRequired(CategoryMetadata.Name_IsRequired).HasMaxLength(CategoryMetadata.Name_MaxLength, CategoryMetadata.Name_IsMaxLength);
			this.Property(o => o.SeoId).IsRequired(CategoryMetadata.SeoId_IsRequired).HasMaxLength(CategoryMetadata.SeoId_MaxLength, CategoryMetadata.SeoId_IsMaxLength);
			this.Property(o => o.Description).IsRequired(CategoryMetadata.Description_IsRequired).HasMaxLength(CategoryMetadata.Description_MaxLength, CategoryMetadata.Description_IsMaxLength);

            this.HasNavigationProperty(o => o.Parent,
                                        o => o.ParentId,
                                        CategoryMetadata.Parent_IsRequired,
                                        CascadeOnDeleteOption.No);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
