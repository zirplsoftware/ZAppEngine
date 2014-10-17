using System;
using System.Collections;
using System.Collections.Generic;
using Zirpl.AppEngine.Model;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Metadata.Membership
{
    public partial class PasswordResetLinkMetadataConstants : MetadataConstantsBase
    {
        public const string User_Name = "User";
		public const bool User_IsRequired = true;

		public const string UserId_Name = "UserId";
		public const bool UserId_IsRequired = true;

        public const string Token_Name = "Token";
		public const bool Token_IsRequired = true;

        public const string Expires_Name = "Expires";
		public const bool Expires_IsRequired = true;

	}
}
