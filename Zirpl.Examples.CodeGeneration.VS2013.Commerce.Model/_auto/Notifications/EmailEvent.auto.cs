using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications
{
    public partial class EmailEvent  : EntityBase<int>
    {
		public virtual DateTime SentDate { get; set; }
		public virtual bool SentSucceeded { get; set; }
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Notifications.EmailEventType EmailEventType { get; set; }
		public virtual byte EmailEventTypeId { get; set; }
		public virtual string Subject { get; set; }
		public virtual string Body { get; set; }
		public virtual string FromEmail { get; set; }
		public virtual string FromName { get; set; }
		public virtual string To { get; set; }
		public virtual string Cc { get; set; }
		public virtual string Bcc { get; set; }
		public virtual DateTime? ResentDate { get; set; }
		public virtual bool? ResentSucceeded { get; set; }
    }
}

