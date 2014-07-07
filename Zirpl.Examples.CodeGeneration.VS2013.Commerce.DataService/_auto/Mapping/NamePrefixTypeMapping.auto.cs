using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping
{
    public partial class NamePrefixTypeMapping : DictionaryEntityMapping<NamePrefixType, byte, NamePrefixTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(NamePrefixTypeMetadata.Name_IsRequired).HasMaxLength(NamePrefixTypeMetadata.Name_MaxLength, NamePrefixTypeMetadata.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
