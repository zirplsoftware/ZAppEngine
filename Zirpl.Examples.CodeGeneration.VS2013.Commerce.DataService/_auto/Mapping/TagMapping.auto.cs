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

			this.Property(o => o.Name).IsRequired(TagMetadataConstants.Name_IsRequired).HasMaxLength(TagMetadataConstants.Name_MaxLength, TagMetadataConstants.Name_IsMaxLength);
			this.Property(o => o.SeoId).IsRequired(TagMetadataConstants.SeoId_IsRequired).HasMaxLength(TagMetadataConstants.SeoId_MaxLength, TagMetadataConstants.SeoId_IsMaxLength);
			this.Property(o => o.Description).IsRequired(TagMetadataConstants.Description_IsRequired).HasMaxLength(TagMetadataConstants.Description_MaxLength, TagMetadataConstants.Description_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
