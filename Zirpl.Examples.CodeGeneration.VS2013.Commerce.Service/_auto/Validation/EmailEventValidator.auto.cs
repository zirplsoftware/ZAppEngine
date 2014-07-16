
using System;
using FluentValidation;
using Zirpl.AppEngine.Validation;
using Zirpl.AppEngine.Validation.EntityFramework;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Service.Validation.Notifications
{
    public partial class EmailEventValidator  : DbEntityValidatorBase<EmailEvent>
		
    {
        public EmailEventValidator()
        {
			this.RuleFor(o => o.SentDate).NotEmpty();
			this.RuleFor(o => o.SentSucceeded).NotNull();
            this.ForeignEntityNotNullAndIdMatches(o => o.EmailEventType, o => o.EmailEventTypeId,
                EmailEventMetadata.EmailEventType_Name, EmailEventMetadata.EmailEventTypeId_Name);
			this.RuleFor(o => o.Subject).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(EmailEventMetadata.Subject_MinLength, EmailEventMetadata.Subject_MaxLength);
			this.RuleFor(o => o.Body).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(EmailEventMetadata.Body_MinLength, EmailEventMetadata.Body_MaxLength);
			this.RuleFor(o => o.FromEmail).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(EmailEventMetadata.FromEmail_MinLength, EmailEventMetadata.FromEmail_MaxLength);
			this.RuleFor(o => o.FromName).Length(EmailEventMetadata.FromName_MinLength, EmailEventMetadata.FromName_MaxLength);
			this.RuleFor(o => o.To).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().Length(EmailEventMetadata.To_MinLength, EmailEventMetadata.To_MaxLength);
			this.RuleFor(o => o.Cc).Length(EmailEventMetadata.Cc_MinLength, EmailEventMetadata.Cc_MaxLength);
			this.RuleFor(o => o.Bcc).Length(EmailEventMetadata.Bcc_MinLength, EmailEventMetadata.Bcc_MaxLength);
			this.When(o =>  o.ResentDate.HasValue, () => {
				this.RuleFor(o => o.ResentDate).NotEmpty();
			});
			this.RuleFor(o => o.ResentSucceeded);

			this.OnCustomValidation();
        }

		partial void OnCustomValidation();
    }
}

