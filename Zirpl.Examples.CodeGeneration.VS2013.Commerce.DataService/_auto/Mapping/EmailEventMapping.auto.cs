using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Notifications
{
    public partial class EmailEventMapping : CoreEntityMappingBase<EmailEvent, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.SentDate).IsRequired(EmailEventMetadata.SentDate_IsRequired).IsDateTime();
			this.Property(o => o.SentSucceeded).IsRequired(EmailEventMetadata.SentSucceeded_IsRequired);

            this.HasNavigationProperty(o => o.EmailEventType,
                                        o => o.EmailEventTypeId,
                                        EmailEventMetadata.EmailEventType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Subject).IsRequired(EmailEventMetadata.Subject_IsRequired).HasMaxLength(EmailEventMetadata.Subject_MaxLength, EmailEventMetadata.Subject_IsMaxLength);
			this.Property(o => o.Body).IsRequired(EmailEventMetadata.Body_IsRequired).HasMaxLength(EmailEventMetadata.Body_MaxLength, EmailEventMetadata.Body_IsMaxLength);
			this.Property(o => o.FromEmail).IsRequired(EmailEventMetadata.FromEmail_IsRequired).HasMaxLength(EmailEventMetadata.FromEmail_MaxLength, EmailEventMetadata.FromEmail_IsMaxLength);
			this.Property(o => o.FromName).IsRequired(EmailEventMetadata.FromName_IsRequired).HasMaxLength(EmailEventMetadata.FromName_MaxLength, EmailEventMetadata.FromName_IsMaxLength);
			this.Property(o => o.To).IsRequired(EmailEventMetadata.To_IsRequired).HasMaxLength(EmailEventMetadata.To_MaxLength, EmailEventMetadata.To_IsMaxLength);
			this.Property(o => o.Cc).IsRequired(EmailEventMetadata.Cc_IsRequired).HasMaxLength(EmailEventMetadata.Cc_MaxLength, EmailEventMetadata.Cc_IsMaxLength);
			this.Property(o => o.Bcc).IsRequired(EmailEventMetadata.Bcc_IsRequired).HasMaxLength(EmailEventMetadata.Bcc_MaxLength, EmailEventMetadata.Bcc_IsMaxLength);
			this.Property(o => o.ResentDate).IsRequired(EmailEventMetadata.ResentDate_IsRequired).IsDateTime();
			this.Property(o => o.ResentSucceeded).IsRequired(EmailEventMetadata.ResentSucceeded_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
