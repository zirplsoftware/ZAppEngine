using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Metadata.Constants;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Constants.Notifications
{
    public partial class EmailEventMetadataConstants : MetadataConstantsBase
    {
        public const string SentDate_Name = "SentDate";
		public const bool SentDate_IsRequired = true;

        public const string SentSucceeded_Name = "SentSucceeded";
		public const bool SentSucceeded_IsRequired = true;

        public const string EmailEventType_Name = "EmailEventType";
		public const bool EmailEventType_IsRequired = true;

		public const string EmailEventTypeId_Name = "EmailEventTypeId";
		public const bool EmailEventTypeId_IsRequired = true;

        public const string Subject_Name = "Subject";
		public const bool Subject_IsRequired = true;
		public const bool Subject_IsMaxLength = false;
        public const int Subject_MinLength = 1;
		public const int Subject_MaxLength = 512;

        public const string Body_Name = "Body";
		public const bool Body_IsRequired = true;
		public const bool Body_IsMaxLength = true;
        public const int Body_MinLength = 1;
		public const int Body_MaxLength = MetadataConstantsBase.MaxLength;

        public const string FromEmail_Name = "FromEmail";
		public const bool FromEmail_IsRequired = true;
		public const bool FromEmail_IsMaxLength = false;
        public const int FromEmail_MinLength = 1;
		public const int FromEmail_MaxLength = 2048;

        public const string FromName_Name = "FromName";
		public const bool FromName_IsRequired = false;
		public const bool FromName_IsMaxLength = false;
        public const int FromName_MinLength = 0;
		public const int FromName_MaxLength = 2048;

        public const string To_Name = "To";
		public const bool To_IsRequired = true;
		public const bool To_IsMaxLength = false;
        public const int To_MinLength = 1;
		public const int To_MaxLength = 2048;

        public const string Cc_Name = "Cc";
		public const bool Cc_IsRequired = false;
		public const bool Cc_IsMaxLength = false;
        public const int Cc_MinLength = 0;
		public const int Cc_MaxLength = 2048;

        public const string Bcc_Name = "Bcc";
		public const bool Bcc_IsRequired = false;
		public const bool Bcc_IsMaxLength = false;
        public const int Bcc_MinLength = 0;
		public const int Bcc_MaxLength = 2048;

        public const string ResentDate_Name = "ResentDate";
		public const bool ResentDate_IsRequired = false;

        public const string ResentSucceeded_Name = "ResentSucceeded";
		public const bool ResentSucceeded_IsRequired = false;

	}
}
