using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Notifications
{
    public partial class EmailEventTypeMapping : DictionaryEntityMapping<EmailEventType, byte, EmailEventTypeEnum>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.Name).IsRequired(EmailEventTypeMetadataConstants.Name_IsRequired).HasMaxLength(EmailEventTypeMetadataConstants.Name_MaxLength, EmailEventTypeMetadataConstants.Name_IsMaxLength);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
