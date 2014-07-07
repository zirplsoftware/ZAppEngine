using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping
{
    public partial class TagMapping : CoreEntityMappingBase<Tag, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(TagMetadata.Name_IsRequired).HasMaxLength(TagMetadata.Name_MaxLength, TagMetadata.Name_IsMaxLength);
			this.Property(o => o.SeoId).IsRequired(TagMetadata.SeoId_IsRequired).HasMaxLength(TagMetadata.SeoId_MaxLength, TagMetadata.SeoId_IsMaxLength);
			this.Property(o => o.Description).IsRequired(TagMetadata.Description_IsRequired).HasMaxLength(TagMetadata.Description_MaxLength, TagMetadata.Description_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
