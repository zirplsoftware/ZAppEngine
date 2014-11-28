using System;
using System.Linq;
using Zirpl.AppEngine.DataService;
using Zirpl.AppEngine.DataService.EntityFramework;
using Zirpl.AppEngine.DataService.EntityFramework.Mapping;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Notifications;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.DataService.Mapping.Notifications
{
    public partial class EmailEventMapping : EntityMappingBase<EmailEvent, int>
    {
		protected override void MapProperties()
        {

			this.Property(o => o.SentDate).IsRequired(EmailEventMetadataConstants.SentDate_IsRequired).IsDateTime();
			this.Property(o => o.SentSucceeded).IsRequired(EmailEventMetadataConstants.SentSucceeded_IsRequired);

            this.HasNavigationProperty(o => o.EmailEventType,
                                        o => o.EmailEventTypeId,
                                        EmailEventMetadataConstants.EmailEventType_IsRequired,
                                        CascadeOnDeleteOption.No);
			this.Property(o => o.Subject).IsRequired(EmailEventMetadataConstants.Subject_IsRequired).HasMaxLength(EmailEventMetadataConstants.Subject_MaxLength, EmailEventMetadataConstants.Subject_IsMaxLength);
			this.Property(o => o.Body).IsRequired(EmailEventMetadataConstants.Body_IsRequired).HasMaxLength(EmailEventMetadataConstants.Body_MaxLength, EmailEventMetadataConstants.Body_IsMaxLength);
			this.Property(o => o.FromEmail).IsRequired(EmailEventMetadataConstants.FromEmail_IsRequired).HasMaxLength(EmailEventMetadataConstants.FromEmail_MaxLength, EmailEventMetadataConstants.FromEmail_IsMaxLength);
			this.Property(o => o.FromName).IsRequired(EmailEventMetadataConstants.FromName_IsRequired).HasMaxLength(EmailEventMetadataConstants.FromName_MaxLength, EmailEventMetadataConstants.FromName_IsMaxLength);
			this.Property(o => o.To).IsRequired(EmailEventMetadataConstants.To_IsRequired).HasMaxLength(EmailEventMetadataConstants.To_MaxLength, EmailEventMetadataConstants.To_IsMaxLength);
			this.Property(o => o.Cc).IsRequired(EmailEventMetadataConstants.Cc_IsRequired).HasMaxLength(EmailEventMetadataConstants.Cc_MaxLength, EmailEventMetadataConstants.Cc_IsMaxLength);
			this.Property(o => o.Bcc).IsRequired(EmailEventMetadataConstants.Bcc_IsRequired).HasMaxLength(EmailEventMetadataConstants.Bcc_MaxLength, EmailEventMetadataConstants.Bcc_IsMaxLength);
			this.Property(o => o.ResentDate).IsRequired(EmailEventMetadataConstants.ResentDate_IsRequired).IsDateTime();
			this.Property(o => o.ResentSucceeded).IsRequired(EmailEventMetadataConstants.ResentSucceeded_IsRequired);

			this.MapCustomProperties();

            base.MapProperties();
        }
		
		partial void MapCustomProperties();
		
    }
}
