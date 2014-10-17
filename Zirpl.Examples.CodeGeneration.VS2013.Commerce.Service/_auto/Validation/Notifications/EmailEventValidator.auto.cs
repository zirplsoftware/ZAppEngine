
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Notifications;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Notifications
{
    public partial class EmailEventValidator  : DbEntityValidatorBase<EmailEvent>
		
    {
        public EmailEventValidator()
        {
			this.RuleFor(o => o.SentDate).NotEmpty();
			this.RuleFor(o => o.SentSucceeded).NotNull();
            this.ForeignEntityNotNullAndIdMatches(o => o.EmailEventType, o => o.EmailEventTypeId,
                EmailEventMetadataConstants.EmailEventType_Name, EmailEventMetadataConstants.EmailEventTypeId_Name);
			this.RuleFor(o => o.Subject).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(EmailEventMetadataConstants.Subject_MinLength, EmailEventMetadataConstants.Subject_MaxLength);
			this.RuleFor(o => o.Body).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(EmailEventMetadataConstants.Body_MinLength, EmailEventMetadataConstants.Body_MaxLength);
			this.RuleFor(o => o.FromEmail).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(EmailEventMetadataConstants.FromEmail_MinLength, EmailEventMetadataConstants.FromEmail_MaxLength);
			this.RuleFor(o => o.FromName).Length(EmailEventMetadataConstants.FromName_MinLength, EmailEventMetadataConstants.FromName_MaxLength);
			this.RuleFor(o => o.To).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(EmailEventMetadataConstants.To_MinLength, EmailEventMetadataConstants.To_MaxLength);
			this.RuleFor(o => o.Cc).Length(EmailEventMetadataConstants.Cc_MinLength, EmailEventMetadataConstants.Cc_MaxLength);
			this.RuleFor(o => o.Bcc).Length(EmailEventMetadataConstants.Bcc_MinLength, EmailEventMetadataConstants.Bcc_MaxLength);
			this.When(o =>  o.ResentDate.HasValue, () => {
				this.RuleFor(o => o.ResentDate).NotEmpty();
			});
			this.RuleFor(o => o.ResentSucceeded);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

